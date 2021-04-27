using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : BossBehaviour
{
    /**
     * This class work on handle enter boss room state
     * 1. Pop up the ! bubble
     * 2. Call the hud and change the text = chit chat dialog
     * 3. Transition to figth state machine
     */

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        talkManager.TriggerExclamationBubble();
        talkManager.TriggerDialogue();
        bossStats.status = Status.Immortal;
        // Add fight as a callback method for event publisher
        EventPublisher.DialogueDone += Fight;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        EventPublisher.DialogueDone -= Fight;
    }

    private void Fight()
    {
        bossStats.status = Status.Mortal;
        animator.SetTrigger("Fight");
    }

}
