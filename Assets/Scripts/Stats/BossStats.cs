using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class BossStats : CharacterStats
{
    [Header("Boss name")]
    public CharacterNameEnum bossName = CharacterNameEnum.Black;
    [Header("Boss Life parameter")]
    [Range(0.0f, 1.0f)]
    public float EnrageRatio;
    [Range(0.0f, 1.0f)]
    public float HyperRatio;
    public BossAggroEnum Aggro = BossAggroEnum.Calm;

    [Header("Default sprite")]
    [SerializeField]
    private Sprite calmSprite;
    [SerializeField]
    private Sprite enrageSprite;
    [SerializeField]
    private Sprite hyperSprite;

    private SpriteRenderer renderer;
    
    public GameObject resemblanceOrbPrefab;

    private ResemblanceManager resemblanceManager;

    private Image healthBorderImage;
    // Start is called before the first frame update
    protected override void Start()
    {
        Teleport.DisablePortal();
        healthBorderImage = healthBar.GetComponent<Image>();
        healthBar.SetActive(true);
        healthBarImage = healthBar.transform.Find("BossHealthBar").GetComponent<Image>();
        base.Start();
        renderer = GetComponent<SpriteRenderer>();
        
        //resemblanceOrbPrefab = Resources.Load("ResemblanceOrb") as GameObject;
        resemblanceManager = resemblanceOrbPrefab.GetComponent<ResemblanceManager>();
        // Work on exception handling below
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeCrashDamage(10);
        }
        if (!healthBar.activeSelf && Aggro != BossAggroEnum.LastStand)
        {
            healthBar.SetActive(true);
        }
        else if (Aggro == BossAggroEnum.LastStand)
        {
            healthBar.SetActive(false);
        }
        
        if (Status == StatusEnum.Immortal)
        {
            healthBorderImage.color = new Color(0.85f, 0.64f, 0.13f);
        }
        else
        {
            healthBorderImage.color = Color.black;
        }
    }

    protected override void GetHealthBarImage()
    {
        healthBarImage.color = Color.green;
    }

    public override void HandleHealth()
    {
        float currentHealthPercentage = GetHealthPercentage();
        HandleHealthBar();
        if (CurrentHealth <= depleteHealth)
        {
            Aggro = BossAggroEnum.LastStand;
            Status = StatusEnum.Immortal;
            Destroy(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>());
            Debug.Log("Death");
            EventPublisher.TriggerStatus(Aggro);
        }
        else if (currentHealthPercentage < HyperRatio && Aggro == BossAggroEnum.Enrage)
        {
            Aggro = BossAggroEnum.Hyper;
            updateSprite(Aggro);
            healthBarImage.color = Color.red;
            EventPublisher.TriggerStatus(Aggro);
        }
        else if (currentHealthPercentage < EnrageRatio && Aggro == BossAggroEnum.Calm)
        {
            Aggro = BossAggroEnum.Enrage;
            updateSprite(Aggro);
            healthBarImage.color = Color.yellow;
            EventPublisher.TriggerStatus(Aggro);
        }
    }

    private void updateSprite(BossAggroEnum aggro)
    {
        switch (aggro)
        {
            case (BossAggroEnum.Calm):
                renderer.sprite = calmSprite;
                break;
            case (BossAggroEnum.Enrage):
                renderer.sprite = enrageSprite;
                break;
            case (BossAggroEnum.Hyper):
                renderer.sprite = hyperSprite;
                break;
            default:
                renderer.sprite = calmSprite;
                break;
        }
    }

    public void TakeCrashDamage(int damage)
    {
        damage -= Armor;

        if (damage < 0) damage = 0;

        CurrentHealth -= damage;
        Debug.Log(transform.name + " -" + damage + " Health left: " + CurrentHealth);
        HandleHealth();
    }

    public override void Die()
    {
        //base.Die();
        Status = StatusEnum.Dead;
        //TODO: Drop/Give item
        Destroy(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>());
        
        Destroy(gameObject);
    }

    public void SpawnResemblance()
    {
        //Do sth
        Instantiate(resemblanceOrbPrefab, transform.position, Quaternion.identity);
    }
}
