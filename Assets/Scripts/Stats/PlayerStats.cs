using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class PlayerStats : CharacterStats
{
    public float RechargeStamina;
    [SerializeField] private float BaseRechargeStamina;
    public float MaxStamina; //Current max stamina
    public float BaseStamina; //Initial base max stamina

    public float DamageMultiplier;

    [SerializeField] private float statMultiplierValue;

    protected override void Start()
    {
        base.Start();
        EventPublisher.DialogueStart += ListenDialogueStart;
        EventPublisher.DialogueDone += ListenDialogueStart;
    }
    
    protected override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        EventPublisher.DialogueStart -= ListenDialogueStart;
        EventPublisher.DialogueDone -= ListenDialogueDone;
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