using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class PlayerStats : CharacterStats
{
    public ColorEnum DebuffColor;
    private float MaxDebuffTime = 0.25f;
    private float DebuffTime;
    private BlackDebuffManager blackDebuffManager;
    private bool isBlackDebuffActivate;

    private Dictionary<ColorEnum, float> MindBreakChargeDict;
    
    public float RechargeStamina;
    [SerializeField] private float BaseRechargeStamina;
    public float MaxStamina; //Current max stamina
    public float BaseStamina; //Initial base max stamina

    public float DamageMultiplier;

    [SerializeField] private float statMultiplierValue;
    public CameraShake CameraShake;

    public SpriteRenderer SpriteRenderer;
    public float MindBreakIncrement;
    private MindManager mindManager;

    protected override void Start()
    {
        base.Start();
        EventPublisher.DialogueStart += ListenDialogueStart;
        EventPublisher.DialogueDone += ListenDialogueStart;
        EventPublisher.StepOnTile += ListenStepOnTile;
        EventPublisher.PlayerFire += OnPlayerFire;

        blackDebuffManager = GetComponentInChildren<BlackDebuffManager>();
        
        CameraShake = GameObject.Find("CameraHolder").GetComponentInChildren<CameraShake>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        DebuffColor = ColorEnum.None;

        MindBreakChargeDict = new Dictionary<ColorEnum, float>();
        MindBreakChargeDict[ColorEnum.Black] = 0f;
        MindBreakChargeDict[ColorEnum.Blue] = 0f;
        mindManager = GameObject.Find("MindBar").GetComponent<MindManager>();
    }

    private void OnPlayerFire(WeaponIDEnum weaponid)
    {
        ColorEnum color = ColorEnum.None;
        switch (weaponid)
        {
            case WeaponIDEnum.Black:
                color = ColorEnum.Black;
                break;
            case WeaponIDEnum.Blue:
                color = ColorEnum.Blue;
                break;
        }

        if (color != ColorEnum.None)
        {
            var mindbreakcharge = MindBreakChargeDict[color];
            UpdateMindBreakValue(color,mindbreakcharge + MindBreakIncrement);
            mindbreakcharge = MindBreakChargeDict[color];

            if (mindbreakcharge >= 100f + MindBreakIncrement)
            {
                MindBreak(color);
            }
        }
    }

    private void MindBreak(ColorEnum color)
    {
        UpdateMindBreakValue(color,0f);
        EventPublisher.TriggerMindBreak(color);
    }

    private void UpdateMindBreakValue(ColorEnum color, float value)
    {
        MindBreakChargeDict[color] = value;
        mindManager.UpdateBar(color, Mathf.Min(1f, (value)/100f));
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        DebuffTime -= Time.fixedDeltaTime;
        if (DebuffTime <= 0f)
        {
            DebuffExit(DebuffColor);
            DebuffColor = ColorEnum.None;
        }

        if (isBlackDebuffActivate && DebuffColor != ColorEnum.Black)
        {
            DebuffExit(ColorEnum.Black);
        }
    }

    private void DebuffExit(ColorEnum debuffColor)
    {
        if (debuffColor == ColorEnum.Black)
        {
            isBlackDebuffActivate = false;
            blackDebuffManager.DebuffExit();
        }
    }

    private void DebuffEnter(ColorEnum debuffColor)
    {
        if (debuffColor == ColorEnum.Black)
        {
            isBlackDebuffActivate = true;
            blackDebuffManager.DebuffEnter();
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(CameraShake.Shake(0.1f, 0.4f));
        StartCoroutine(Flash(SpriteRenderer,0.5f, 4));
    }

    private void OnDestroy()
    {
        EventPublisher.DialogueStart -= ListenDialogueStart;
        EventPublisher.DialogueDone -= ListenDialogueDone;
        EventPublisher.StepOnTile -= ListenStepOnTile;
        EventPublisher.PlayerFire -= OnPlayerFire;
    }

    private void ListenStepOnTile(ColorEnum colorenum)
    {
        
        if (DebuffColor != colorenum)
        {
            DebuffEnter(colorenum);
        }
        DebuffColor = colorenum;
        DebuffTime = MaxDebuffTime;
    }

    protected override void GetHealthBarImage()
    {
        if (healthBar == null)
            healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        healthBarImage = healthBar.GetComponent<Image>();
    }
    public void ApplyStatBuff(ResemblanceBuffEnum buff)
    {
        switch (buff)
        {
            case ResemblanceBuffEnum.IncreaseHealth:
                setMaxHealth((int) (BaseMaxHealth * statMultiplierValue));
                PlayerConfig.healValue = MaxHealth;
                break;
            case ResemblanceBuffEnum.IncreaseStamina:
                RechargeStamina = (BaseRechargeStamina * statMultiplierValue);
                break;
            case ResemblanceBuffEnum.IncreaseDamage:
                DamageMultiplier = statMultiplierValue;
                break;
        }


    }

    public void ListenDialogueStart()
    {
        Status = StatusEnum.Immortal;
    }
    
    public void ListenDialogueDone()
    {
        Status = StatusEnum.Mortal;
    }

    public override void HealthRunOut()
    {
        GameManager.Instance.HandleGameOver();
        // SceneManager.LoadScene("MainMenuScene");
    }

    public void shakeCamera()
    {
        StartCoroutine(CameraShake.Shake(0.1f, 0.4f));
    }
    
}