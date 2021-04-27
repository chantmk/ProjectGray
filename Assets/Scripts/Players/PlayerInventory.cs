using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    //public Text ui;
    public Text resemblanceCountText;
    [SerializeField] private int healthPackCount;
    [SerializeField] private int resemblanceCount;

    [SerializeField] public GameObject player;
    [SerializeField] public PlayerStats playerStats;

    [SerializeField] private float healValue;

    private List<Item> items = new List<Item>();

    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();

        //resemblanceCountText.text = $"Resemblance Count {resemblanceCount}";
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            UseHealthPack(1);
            AddResemblance(1);
        }
    }

    public void AddHealthPack(int amount)
    {
        healthPackCount += amount;
        Debug.Log("Total health pack " + healthPackCount);
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

    public int GetHealthPackCount()
    {
        return healthPackCount;
    }

    public void AddResemblance(int amount)
    {
        resemblanceCount += amount;
        //resemblanceCountText.text = $"Resemblance Count {resemblanceCount}";
    }

    public bool UseResemblance(int amount)
    {
        if (resemblanceCount < amount) return false;

        resemblanceCount -= amount;
        return true;
    }
    
    public int GetResemblanceCount()
    {
        return resemblanceCount;
    }

}
