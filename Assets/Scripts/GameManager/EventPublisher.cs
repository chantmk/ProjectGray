using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EventPublisher
{
    // Player delegates
    //public delegate void OnPlayerJump();
    public delegate void OnPlayerPressFire();
    public delegate void OnPlayerFire(WeaponIDEnum weaponID);
    
    public static event OnPlayerPressFire PlayerPressFire;
    public static event OnPlayerFire PlayerFire;
    

    public static void TriggerPlayerPressFire()
    {
        PlayerPressFire?.Invoke();
    }

    public static void TriggerPlayerFire(WeaponIDEnum weaponID)
    {
        PlayerFire?.Invoke(weaponID);
    }

    public delegate void OnStepOnTile(ColorEnum colorEnum);

    public static event OnStepOnTile StepOnTile;

    public static void TriggerStepOnTile(ColorEnum colorEnum)
    {
        StepOnTile?.Invoke(colorEnum);
    }

    public delegate void OnStatusChange(BossAggroEnum bossStatus);
    public static event OnStatusChange StatusChange;
    public static void TriggerStatus(BossAggroEnum bossStatus)
    {
        StatusChange?.Invoke(bossStatus);
    }



    public delegate void OnParticleSpawn(ParticleEnum particleEnum, Vector2 position);

    public static event OnParticleSpawn ParticleSpawn;

    public static void TriggerParticleSpawn(ParticleEnum particleEnum, Vector2 position)
    {
        ParticleSpawn?.Invoke(particleEnum, position);
    }
    
    public delegate void OnBlueBubbleDestroy(Vector3 position);
    public static event OnBlueBubbleDestroy BlueBubbleDestroy;
    public static void TriggerBlueBubbleDestroy(Vector3 position)
    {
        BlueBubbleDestroy?.Invoke(position);
    }

    // Game event
    public delegate void OnPlayCutScene();
    public static event OnPlayCutScene PlayCutscene;
    public static void TriggerPlayCutScene()
    {
        PlayCutscene?.Invoke();
    }

    public delegate void OnEndCutScene();
    public static event OnEndCutScene EndCutscene;
    public static void TriggerEndCutScene()
    {
        EndCutscene?.Invoke();
    }

    public delegate void OnDecisionMake(DecisionEnum decision, CharacterNameEnum bossName);
    public static event OnDecisionMake DecisionMake;
    public static void TriggerDecisionMake(DecisionEnum decision, CharacterNameEnum bossName)
    {
        DecisionMake?.Invoke(decision, bossName);
    }

    public delegate void OnDialogueStart();
    public static event OnDialogueStart DialogueStart;
    public static void TriggerDialogueStart()
    {
        DialogueStart?.Invoke();
    }

    public delegate void OnDialogueDone();
    public static event OnDialogueDone DialogueDone;

    public static void TriggerDialogueDone()
    {
        DialogueDone?.Invoke();
    }

    public delegate void OnMindBreak(ColorEnum color);
    public static event OnMindBreak MindBreak;
    
    public static void TriggerMindBreak(ColorEnum color)
    {
        MindBreak?.Invoke(color);
    }
    
    public delegate void OnGuardianCall(ColorEnum color);
    public static event OnGuardianCall GuardianCall;
    
    public static void TriggerGuardianCall(ColorEnum color)
    {
        GuardianCall?.Invoke(color);
    }
    
    public delegate void OnSetGuardianUI(ColorEnum color, bool enable);
    public static event OnSetGuardianUI SetGuardianUI;
    
    public static void TriggerSetGuardianUI(ColorEnum color, bool enable)
    {
        SetGuardianUI?.Invoke(color, enable);
    }

    public delegate void OnGateEnable(bool enable);
    public static event OnGateEnable GateEnable;
    public static void TriggerGateEnable(bool enable)
    {
        GateEnable?.Invoke(enable);
    }
}
