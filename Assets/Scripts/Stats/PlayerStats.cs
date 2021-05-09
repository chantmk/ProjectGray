using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [SerializeField] private GameObject healthBar;
    private Image healthBarImage;

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

}