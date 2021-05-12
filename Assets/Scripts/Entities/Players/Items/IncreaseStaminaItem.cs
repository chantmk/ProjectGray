using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStaminaItem : StatItem
{
    public override void OnInteract()
    {
        player.GetComponent<PlayerInventory>().AddBuff(ResemblanceBuffEnum.IncreaseStamina);
        Destroy(gameObject);
        Debug.Log("Received Increase stamina buff");
    }
}
