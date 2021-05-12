using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackManager : Interactable
{
    public GameObject player;
    [SerializeField] private float dropChance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    
    public override void OnInteract()
    {
        player.GetComponent<PlayerInventory>().AddHealthPack(1);
        Destroy(gameObject);
    }

    public float GetDropChance()
    {
        return dropChance;
    }
}
