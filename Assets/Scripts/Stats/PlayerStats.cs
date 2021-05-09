using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [SerializeField] private GameObject healthBar;
    private Image healthBarImage;

    public float RechargeStamina;
    public float MaxStamina; //Current max stamina
    public float BaseStamina; //Initial base max stamina

    public float DamageMultiplier;

    protected override void Start()
    {
        base.Start();
        if (healthBar == null)
            healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        healthBarImage = healthBar.GetComponent<Image>();

        //AddStatBuff(new MaxHealthIncreaseBuff());

    }
    
    protected override void Update()
    {
        base.Update();
        healthBarImage.fillAmount = GetHealthPercentage();
    }

    public void ApplyStatBuff(ResemblanceBuffEnum buff)
    {
        switch (buff)
        {
            case ResemblanceBuffEnum.IncreaseHealth:
                setMaxHealth(BaseMaxHealth * 1.5f);
                break;
            case ResemblanceBuffEnum.IncreaseStamina:
                RechargeStamina *= 1.5f;
                break;
            case ResemblanceBuffEnum.IncreaseDamage:
                DamageMultiplier *= 1.5f;
                break;
        }


    }
}