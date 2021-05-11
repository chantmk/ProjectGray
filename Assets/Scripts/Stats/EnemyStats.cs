using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Utils;

public class EnemyStats : CharacterStats
{
    private static GameObject healthPackPrefab;

    private HealthPackManager healthPackManager;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        healthPackPrefab = Resources.Load("Healthpack") as GameObject;
        healthPackManager = healthPackPrefab.GetComponent<HealthPackManager>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        Status = StatusEnum.Dead;
        if (Random.Range(0.0f, 1.0f) <= healthPackManager.GetDropChance())
        {
            Instantiate(healthPackPrefab, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
