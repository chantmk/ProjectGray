using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    
    public Text ui;
    [SerializeField] private int healthPackCount;
    [SerializeField] private int resemblanceCount;

    [SerializeField] public GameObject player;
    [SerializeField] public PlayerStats playerStats;

    [SerializeField] private float healValue;

    // Start is called before the first frame update
    void Start()
    {
        // healthPackCount = 10;
        resemblanceCount = 0;
        healValue = 100f;
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        
        ui.text = "+ 0";

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

        // for (int i = 0; i < amount; ++i)
        // {
        //     playerStats.Heal(healValue);
        // }
        playerStats.Heal(100);
        healthPackCount -= 1;
        ui.text = $"+ {healthPackCount}";
        
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
        ui.text = $"+ {healthPackCount}";
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
