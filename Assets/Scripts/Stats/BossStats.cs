using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BossStatus
{
    Calm,
    Enrage,
    Hyper,
    LastStand
}

public class BossStats : CharacterStats
{
    [Header("Boss Life parameter")]
    [Range(0.0f, 1.0f)]
    public float EnrageRatio;
    [Range(0.0f, 1.0f)]
    public float HyperRatio;
    public BossStatus Aggro = BossStatus.Calm;
    [SerializeField]
    private GameObject healthBar;

    private Image healthBarImage;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        healthBar.transform.parent.gameObject.SetActive(true);
        healthBarImage = healthBar.GetComponent<Image>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        healthBarImage.fillAmount = GetHealthPercentage();
    }

    public override void HandleHealth()
    {
        float currentHealthPercentage = GetHealthPercentage();
        if (currentHealth <= depleteHealth)
        {
            Aggro = BossStatus.LastStand;
            status = Status.Immortal;
        }
        else if (currentHealthPercentage < HyperRatio)
        {
            Aggro = BossStatus.Hyper;
        }
        else if (currentHealthPercentage < EnrageRatio)
        {
            Aggro = BossStatus.Enrage;
        }
        EventPublisher.TriggerStatus(Aggro);
    }
}
