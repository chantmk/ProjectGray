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
    
    public float RechargeStamina;
    [SerializeField] private float BaseRechargeStamina;
    public float MaxStamina; //Current max stamina
    public float BaseStamina; //Initial base max stamina

    public float DamageMultiplier;

    [SerializeField] private float statMultiplierValue;
    public CameraShake CameraShake;

    public SpriteRenderer SpriteRenderer;
    protected override void Start()
    {
        base.Start();
        EventPublisher.DialogueStart += ListenDialogueStart;
        EventPublisher.DialogueDone += ListenDialogueStart;
        EventPublisher.StepOnTile += ListenStepOnTile;

        blackDebuffManager = GetComponentInChildren<BlackDebuffManager>();
        
        CameraShake = GameObject.Find("CameraHolder").GetComponentInChildren<CameraShake>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        DebuffColor = ColorEnum.None;
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
    }

    private void DebuffExit(ColorEnum debuffColor)
    {
        if (debuffColor == ColorEnum.Black)
        {
            blackDebuffManager.DebuffExit();
        }
    }

    private void DebuffEnter(ColorEnum debuffColor)
    {
        if (debuffColor == ColorEnum.Black)
        {
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
    }

    private void ListenStepOnTile(ColorEnum colorenum)
    {
        print(colorenum);
        
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

    
}