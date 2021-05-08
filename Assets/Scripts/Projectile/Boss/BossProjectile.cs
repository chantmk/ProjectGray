using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BossProjectile : EnemyProjectile
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        EventPublisher.DecisionMake += OnDecisionMakeHandler;
    }

    private void OnDestroy()
    {
        EventPublisher.DecisionMake -= OnDecisionMakeHandler;
    }

    public void OnDecisionMakeHandler(DecisionEnum decision)
    {
        Execute();
    }
}
