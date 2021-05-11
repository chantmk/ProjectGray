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

    //Player stats delegates
    //public delegate void OnPlayerTakeDamage();

    // Player events
    //public static event OnPlayerJump PlayerJump;
    public static event OnPlayerPressFire PlayerPressFire;
    public static event OnPlayerFire PlayerFire;

    //public static event OnPlayerTakeDamage PlayerTakeDamage;

    //public static void TriggerPlayerJump()
    //{
    //    PlayerJump?.Invoke();
    //}

    public static void TriggerPlayerPressFire()
    {
        PlayerPressFire?.Invoke();
    }

    public static void TriggerPlayerFire(WeaponIDEnum weaponID)
    {
        PlayerFire?.Invoke(weaponID);
    }

    //pulblic static void TriggerPlayerTakeDamage()
    //{
    //    PlayerTakeDamage?.Invoke();
    //}

    public delegate void OnDialogueDone();
    public static event OnDialogueDone DialogueDone;

    public static void TriggerDialogueDone()
    {
        DialogueDone?.Invoke();
    }

    public delegate void OnStatusChange(BossAggroEnum bossStatus);
    public static event OnStatusChange StatusChange;
    public static void TriggerStatus(BossAggroEnum bossStatus)
    {
        StatusChange?.Invoke(bossStatus);
    }

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

    public delegate void OnDecisionMake(DecisionEnum decision);
    public static event OnDecisionMake DecisionMake;
    public static void TriggerDecisionMake(DecisionEnum decision)
    {
        DecisionMake?.Invoke(decision);
    }

    public delegate void OnParticleSpawn(ParticleEnum particleEnum, Vector2 position);

    public static event OnParticleSpawn ParticleSpawn;

    public static void TriggerParticleSpawn(ParticleEnum particleEnum, Vector2 position)
    {
        ParticleSpawn?.Invoke(particleEnum, position);
    }
}
