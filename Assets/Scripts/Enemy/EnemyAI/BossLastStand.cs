using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLastStand : BossBehaviour
{
    /**
     * This class work on boss immortal, pop up mercy text and decision making
     * 1. Popup ... balloon
     * 2. Play cutscene
     * 3. Decision popup
     */
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // Immortal
        bossStats.status = Status.Immortal;
        talkManager.TriggerDotBubble();
        askForMercy();
        //EventPublisher.DialogueDone += askForMercy;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        //EventPublisher.DialogueDone -= askForMercy;
    }

    private void askForMercy()
    {
        // Pop up Decision maker HUD
        Debug.Log("Ask For Mercy");
        EventPublisher.TriggerPlayCutScene();
    }
}
