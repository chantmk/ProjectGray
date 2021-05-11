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
        enemyRigidbody.velocity = Vector2.zero;
        EventPublisher.EndCutscene += AskForMercy;
        EventPublisher.DialogueDone += DialogueDoneHandler;

        talkManager.TriggerDotBubble();
        talkManager.TriggerDialogue(DialogueStateEnum.LastStand);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        EventPublisher.EndCutscene -= AskForMercy;
        EventPublisher.DialogueDone -= DialogueDoneHandler;
    }

    protected void AskForMercy()
    {
        talkManager.TriggerDialogue(DialogueStateEnum.Decision);
    }

    protected virtual void DialogueDoneHandler()
    {
        if (DialogueManager.currentDialogueState == DialogueStateEnum.LastStand)
        {
            EventPublisher.TriggerPlayCutScene();
        }
        else if (DialogueManager.currentDialogueState == DialogueStateEnum.Mercy)
        {
            bossStats.Status = StatusEnum.Dead;
            animator.SetInteger(AnimatorParams.Decision, (int)DecisionEnum.Mercy);
            animator.SetInteger(AnimatorParams.Life, (int)bossStats.Status);
        }
        else if (DialogueManager.currentDialogueState == DialogueStateEnum.Kill)
        {
            bossStats.Status = StatusEnum.Dead;
            animator.SetInteger(AnimatorParams.Decision, (int)DecisionEnum.Kill);
            animator.SetInteger(AnimatorParams.Life, (int)bossStats.Status);
        }
    }
}
