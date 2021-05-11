using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class EnemyStats : CharacterStats
{
    protected override void GetHealthBarImage()
    {
        if (healthBar == null)
        {
            healthBar = transform.Find("HealthBarContainer").gameObject;
        }
        healthBarImage = healthBar.transform.Find("Health").GetComponent<Image>();
    }

    public override void HealthRunOut()
    {
        Status = StatusEnum.Dead;
    }
}
