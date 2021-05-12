using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageItem : StatItem
{
    public override void OnInteract()
    {
        player.GetComponent<PlayerInventory>().AddBuff(ResemblanceBuffEnum.IncreaseDamage);
        PlayerConfig.DamageMultiplier = 2;
        Destroy(gameObject);
        Debug.Log("Received Increase damage buff");
    }
}
