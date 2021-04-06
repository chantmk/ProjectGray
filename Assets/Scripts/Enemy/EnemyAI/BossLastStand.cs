using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLastStand : BossBehaviour
{
    // This class work on boss immortal, pop up mercy text and decision making
    // Kill will only play dead animation
    // While at this state mercy will pop up dialogue
    private bool Chose = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // Immortal
        boss.IsImmortal = true;
        askForMercy();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        ListenToLifeSignal();
    }
    private void ListenToLifeSignal()
    {
        if (boss.IsMercy && !Chose)
        {
            animator.SetTrigger("Mercy");
            Chose = true;
        }
        else if (boss.IsKill && !Chose)
        {
            animator.SetTrigger("Kill");
            Chose = true;
        }
    }

    private void askForMercy()
    {

    }
}
