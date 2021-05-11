using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealthItem : StatItem
{
    public override void OnInteract()
    {
        player.GetComponent<PlayerInventory>().AddBuff(ResemblanceBuffEnum.IncreaseHealth);
        Destroy(gameObject);
        Debug.Log("Received Increase health buff");
    }
}
