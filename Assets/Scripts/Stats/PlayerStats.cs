using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

public class PlayerStats : CharacterStats
{
    public ColorEnum DebuffColor;
    private float MaxDebuffTime = 0.25f;
    private float DebuffTime;
    private BlackDebuffManager blackDebuffManager;
    private bool isBlackDebuffActivate;
    public float MindBreakDecayRate;
    public float MindBreakDecayCD;
    public float GuardianCD;

    private Dictionary<ColorEnum, float> MindBreakChargeDict;
    private Dictionary<ColorEnum, float> MindBreakDecayCDDict;

    private Dictionary<ColorEnum, float> GuardianCDDict;

    public float RechargeStamina;
    [SerializeField] private float BaseRechargeStamina;
    public float MaxStamina; //Current max stamina
    public float BaseStamina; //Initial base max stamina

    public float DamageMultiplier;

    [SerializeField] private float statMultiplierValue;
    public CameraShake CameraShake;

    public SpriteRenderer spriteRenderer;
    public float MindBreakIncrement;
    private MindManager mindManager;
    
    private int immortalBuffCount;
    private Color originalColor;

    public int ImmortalBuffCount
    {
        get { return immortalBuffCount; }
        set
        {
            
            immortalBuffCount = Mathf.Max(value, 0);
            if (immortalBuffCount > 0)
            {
                var newcolor = originalColor;
                newcolor.a = 0.5f;

                spriteRenderer.color = newcolor;
                // Status = StatusEnum.Immortal;
            }
            else
            {
                spriteRenderer.color = originalColor;
                // Status = StatusEnum.Mortal;
            }
        }

    }

    protected override void Start()
    {
        base.Start();
        EventPublisher.DialogueStart += ListenDialogueStart;
        EventPublisher.DialogueDone += ListenDialogueStart;
        EventPublisher.StepOnTile += ListenStepOnTile;
        EventPublisher.PlayerFire += OnPlayerFire;

        blackDebuffManager = GetComponentInChildren<BlackDebuffManager>();
        
        CameraShake = GameObject.Find("CameraHolder").GetComponentInChildren<CameraShake>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        DebuffColor = ColorEnum.None;

        MindBreakChargeDict = new Dictionary<ColorEnum, float>();
        MindBreakChargeDict[ColorEnum.Black] = 0f;
        MindBreakChargeDict[ColorEnum.Blue] = 0f;
        
        MindBreakDecayCDDict = new Dictionary<ColorEnum, float>();
        MindBreakDecayCDDict[ColorEnum.Black] = MindBreakDecayCD;
        MindBreakDecayCDDict[ColorEnum.Blue] = MindBreakDecayCD;
        
        GuardianCDDict = new Dictionary<ColorEnum, float>();
        GuardianCDDict[ColorEnum.Black] = 0f;
        GuardianCDDict[ColorEnum.Blue] = 0f;
        
        mindManager = GameObject.Find("MindBar").GetComponent<MindManager>();

        originalColor = spriteRenderer.color;
        ImmortalBuffCount = 0;
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
            AddMindBreakValue(color, MindBreakIncrement);
        }
    }

    private void MindBreak(ColorEnum color)
    {
        if (GuardianCDDict[color] <= 0f && PlayerConfig.HaveResemblanceDict[color])
        {
            EventPublisher.TriggerGuardianCall(color);
            GuardianCDDict[color] = GuardianCD;
        }
        
        UpdateMindBreakValue(color,0f);
        EventPublisher.TriggerMindBreak(color);
    }

    private void AddMindBreakValue(ColorEnum color, float value)
    {
        MindBreakDecayCDDict[color] = MindBreakDecayCD;
        var currentValue = MindBreakChargeDict[color];
        var newValue = currentValue + value;
        UpdateMindBreakValue(color, newValue);
        
        if (newValue >= 100f + MindBreakIncrement)
        {
            MindBreak(color);
        }
    }
    
    private void SubtractMindBreakValule(ColorEnum color, float value)
    {
        var currentValue = MindBreakChargeDict[color];
        var newValue = Mathf.Max(currentValue - value, 0f);
        
        UpdateMindBreakValue(color, newValue);
    }
    private void UpdateMindBreakValue(ColorEnum color, float value)
    {
        MindBreakChargeDict[color] = value;
        mindManager.UpdateBar(color, Mathf.Max(0f,Mathf.Min(1f, (value)/100f)));
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

        var keylist = MindBreakDecayCDDict.Keys.ToList();
        foreach (var color in keylist)
        {
            var newdecayCD = MindBreakDecayCDDict[color] - Time.fixedDeltaTime;
            MindBreakDecayCDDict[color] = newdecayCD;
            if (newdecayCD < 0)
            {
                SubtractMindBreakValule(color, MindBreakDecayRate*Time.fixedDeltaTime);
            }

            if (GuardianCDDict[color] > 0f)
            {
                GuardianCDDict[color] -= Time.fixedDeltaTime;
            }

            if (GuardianCDDict[color] <= 0f && PlayerConfig.HaveResemblanceDict[color])
            {
                EventPublisher.TriggerSetGuardianUI(color,true);
            }
            else
            {
                EventPublisher.TriggerSetGuardianUI(color, false);
            }
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
        print("player collide");
        base.TakeDamage(damage);
        if (Status == StatusEnum.Mortal)
        {
            StartCoroutine(CameraShake.Shake(0.1f, 0.4f));
            StartCoroutine(SetImmortalDelay(1.5f));
        }
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
    
    public IEnumerator SetImmortalDelay(float duration )
    {
        ImmortalBuffCount += 1;
        yield return new WaitForSeconds(duration);
        ImmortalBuffCount -= 1;
    }
    
}