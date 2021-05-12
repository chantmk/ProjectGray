using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    //public Text ui;
    public Text resemblanceCountText;
    //public Text redeemAmountText;
    public Text healthPackText; // For inventory UI
    public Text resemblanceText; // For inventory UI
    public GameObject resemblanceUI;
    public GameObject inventoryBox;

    [SerializeField] private int healthPackCount;
    [SerializeField] private int resemblanceCount;

    [SerializeField] public GameObject player;
    [SerializeField] public PlayerStats playerStats;

    [SerializeField] private int healValue;

    private List<ResemblanceBuffEnum> buffs = new List<ResemblanceBuffEnum>();

    public ParticleSystem HealingParticle;
    public AudioClip healSound;
    public float healVolume = 1f;
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        resemblanceUI = GameObject.Find("ResemblanceUI");
        inventoryBox = GameObject.Find("InventoryBox");
        
        resemblanceCountText = resemblanceUI.transform.Find("ResemblanceCountText").GetComponent<Text>();
        healthPackText = inventoryBox.transform.Find("HealthPackText").GetComponent<Text>();
        resemblanceText = inventoryBox.transform.Find("ResemblanceText").GetComponent<Text>();
        audioSrc = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioSource>();

        resemblanceUI.SetActive(false);
        audioSrc = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioSource>();


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
        PlayerConfig.HealthPackCount = healthPackCount;
        Debug.Log("Total health pack " + healthPackCount);
        healthPackText.text = $"{healthPackCount}";
    }

    public bool UseHealthPack(int amount)
    {
        audioSrc.PlayOneShot(healSound, healVolume);
        if (healthPackCount < amount) return false;

        for (int i = 0; i < amount; ++i)
        {
            playerStats.Heal(healValue);
        }
        healthPackCount -= amount;
        healthPackText.text = $"{healthPackCount}";
        
        //Play healing particle
        EventPublisher.TriggerParticleSpawn(ParticleEnum.HealingParticle, transform.position);
        return true;
    }

    public int GetHealthPackCount()
    {
        return healthPackCount;
    }

    public void AddResemblance(int amount)
    {
        resemblanceCount += amount;
        resemblanceCountText.text = $"Resemblance Count {resemblanceCount}";
        resemblanceText.text = $"{resemblanceCount}";
    }

    public bool UseResemblance(int amount)
    {
        if (resemblanceCount < amount) return false;

        resemblanceCount -= amount;
        Debug.Log("Redeem "+amount+" resemblances");
        resemblanceCountText.text = $"Resemblance Count {resemblanceCount}";
        resemblanceText.text = $"{resemblanceCount}";
        return true;
    }
    
    public int GetResemblanceCount()
    {
        return resemblanceCount;
    }

    public void AddBuff(ResemblanceBuffEnum buff)
    {
        buffs.Add(buff);
        ApplyAllBuff();
    }

    private void ApplyAllBuff()
    {
        foreach (var buff in buffs)
        {
            playerStats.ApplyStatBuff(buff);
        }

    }

}
