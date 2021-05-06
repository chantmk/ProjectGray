using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

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
        bossStats.Status = StatusEnum.Immortal;
        talkManager.TriggerDotBubble();
        EventPublisher.TriggerPlayCutScene();

        EventPublisher.EndCutscene += askForMercy;
        EventPublisher.DecisionMake += DecisionHandler;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        EventPublisher.EndCutscene -= askForMercy;
        EventPublisher.DecisionMake -= DecisionHandler;
    }

    private void askForMercy()
    {
        // Pop up Decision maker HUD
        Debug.Log("Ask For Mercy");
        talkManager.TriggerDecision();
    }

    void DecisionHandler(DecisionEnum decision)
    {
        animator.SetInteger(AnimatorParams.Decision, (int)decision);
        bossStats.Status = StatusEnum.Dead;
        animator.SetInteger(AnimatorParams.Life, (int)bossStats.Status);
    }
}
