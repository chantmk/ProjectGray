using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class BossStats : CharacterStats
{
    [Header("Boss Life parameter")]
    [Range(0.0f, 1.0f)]
    public float EnrageRatio;
    [Range(0.0f, 1.0f)]
    public float HyperRatio;
    public BossAggroEnum Aggro = BossAggroEnum.Calm;
    [SerializeField]
    private GameObject healthBarContainer;

    private Image healthBarImage;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        healthBarContainer.SetActive(true);
        healthBarImage = healthBarContainer.GetComponentInChildren<Image>();
        healthBarImage.color = Color.green;
        // Work on exception handling below
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(!healthBarContainer.activeSelf && Aggro != BossAggroEnum.LastStand)
        {
            healthBarContainer.SetActive(true);
        }
        healthBarImage.fillAmount = GetHealthPercentage();
    }

    public override void HandleHealth()
    {
        float currentHealthPercentage = GetHealthPercentage();
        if (CurrentHealth <= depleteHealth)
        {
            Aggro = BossAggroEnum.LastStand;
            Status = StatusEnum.Immortal;
            Debug.Log("Death");
            EventPublisher.TriggerStatus(Aggro);
        }
        else if (currentHealthPercentage < HyperRatio && Aggro == BossAggroEnum.Enrage)
        {
            Aggro = BossAggroEnum.Hyper;
            healthBarImage.color = Color.red;
            EventPublisher.TriggerStatus(Aggro);
        }
        else if (currentHealthPercentage < EnrageRatio && Aggro == BossAggroEnum.Calm)
        {
            Aggro = BossAggroEnum.Enrage;
            healthBarImage.color = Color.yellow;
            EventPublisher.TriggerStatus(Aggro);
        }
    }

    public void TakeCrashDamage(float damage)
    {
        damage -= Armor;

        if (damage < GrayConstants.EPSILON) damage = 0.0f;

        CurrentHealth -= damage;
        Debug.Log(transform.name + " -" + damage + " Health left: " + CurrentHealth);
        HandleHealth();
    }

    public override void Die()
    {
        base.Die();
        // Drop/Give item
    }
}
