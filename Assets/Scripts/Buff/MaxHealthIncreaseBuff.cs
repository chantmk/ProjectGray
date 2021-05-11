using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthIncreaseBuff : StatBuff
{
    [SerializeField] private int targetMaxHealth = 10;

    public void Start()
    {
        buffName = "Max Health Buff";
        isPermanentBuff = true;
        maxBuffDuration = 999;
        buffDuration = 999;
        isApplied = false ;
        isFinished = false;
}

    public override void End()
    {
        // Do sth if end
    }

    public override void Apply(CharacterStats characterStats)
    {
        if (!isApplied)
        {
            isApplied = true;
            characterStats.setMaxHealth(targetMaxHealth);
            Debug.Log("Add max health");
            Debug.Log(characterStats.GetHealthPercentage());
            
        }
    }
}
