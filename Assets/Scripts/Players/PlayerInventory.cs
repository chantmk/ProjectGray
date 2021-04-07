using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int healthPackCount;
    [SerializeField] private int resemblanceCount;

    [SerializeField] public GameObject player;
    [SerializeField] public PlayerStats playerStats;

    [SerializeField] private float healValue;

    // Start is called before the first frame update
    void Start()
    {
        healthPackCount = 10;
        resemblanceCount = 0;
        healValue = 10f;
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            UseHealthPack(1);
        }
    }

    public bool UseHealthPack(int amount)
    {
        if (healthPackCount < amount) return false;

        for (int i = 0; i < amount; ++i)
        {
            playerStats.Heal(healValue);
        }
        healthPackCount -= amount;
        
        return true;
    }

    public bool UseResemblance(int amount)
    {
        if (resemblanceCount < amount) return false;

        resemblanceCount -= amount;
        return true;
    }

    public void AddHealthPack()
    {
        print("Add health pack");
        healthPackCount += 1;
    }
    public int GetHealthPackCount()
    {
        return healthPackCount;
    }

    public int GetResemblanceCount()
    {
        return resemblanceCount;
    }

}
