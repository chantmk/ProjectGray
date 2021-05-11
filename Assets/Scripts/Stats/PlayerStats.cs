using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [SerializeField] private GameObject healthBar;
    private Image healthBarImage;

    public float RechargeStamina;
    [SerializeField] private float BaseRechargeStamina;
    public float MaxStamina; //Current max stamina
    public float BaseStamina; //Initial base max stamina

    public float DamageMultiplier;

    [SerializeField] private float statMultiplierValue;

    protected override void Start()
    {
        base.Start();
        if (healthBar == null)
            healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        healthBarImage = healthBar.GetComponent<Image>();
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
                setMaxHealth((int) (BaseMaxHealth * statMultiplierValue));
                break;
            case ResemblanceBuffEnum.IncreaseStamina:
                RechargeStamina = (BaseRechargeStamina * statMultiplierValue);
                break;
            case ResemblanceBuffEnum.IncreaseDamage:
                DamageMultiplier = statMultiplierValue;
                break;
        }


    }

    public override void Die()
    {
        GameManager.Instance.HandleGameOver();
        // SceneManager.LoadScene("MainMenuScene");
    }
}