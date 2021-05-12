using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedeemButtonHandler : MonoBehaviour
{
    private Transform resemblancePad;
    private GameObject player;
    private PlayerInventory playerInventory;
    
    private int playerResemblanceCount;
    private readonly int[] checkpointAmount = {1, 2, 3 };

    public GameObject DoubleDamagePrefab;
    public GameObject SpeedShoePrefab;
    public GameObject HealthArmorPrefab;
    
    public void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<PlayerInventory>();
        resemblancePad = GameObject.Find ("ResemblancePad").transform;
    }

    public void OnButtonPress()
    {
        playerResemblanceCount = playerInventory.GetResemblanceCount();
        
        if (playerResemblanceCount >= checkpointAmount[2])
        {
            // Redeem 6 resemblances
            if (playerInventory.UseResemblance(checkpointAmount[2]))
            {
                //TODO : spawn DoubleDamage Item
                Instantiate(DoubleDamagePrefab, resemblancePad.position + new Vector3(2,-2,0), Quaternion.identity);
            }
            else
            {
                Debug.Log("Failed to redeem 6");
            }

        }
        else if (playerResemblanceCount >= checkpointAmount[1])
        {
            // Redeem 4 resemblances
            if (playerInventory.UseResemblance(checkpointAmount[1]))
            {
                //TODO : spawn SpeedShoe Item
                Instantiate(SpeedShoePrefab, resemblancePad.position + new Vector3(2,-2,0), Quaternion.identity);
            }
            else
            {
                Debug.Log("Failed to redeem 4");
            }

        }
        else
        {
            // Redeem 2 resemblances
            if (playerInventory.UseResemblance(checkpointAmount[0]))
            {
                //TODO : spawn HealthArmor Item
                Instantiate(HealthArmorPrefab, resemblancePad.position + new Vector3(2,-2,0), Quaternion.identity);
            }
            else
            {
                Debug.Log("Failed to redeem 2");
            }

        }
    }
}
