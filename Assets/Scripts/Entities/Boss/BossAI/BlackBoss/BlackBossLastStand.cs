using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BlackBossLastStand : BossLastStand
{
    protected override void DialogueDoneHandler()
    {
        if (DialogueManager.currentDialogueState == DialogueStateEnum.LastStand)
        {
            AskForMercy();
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
