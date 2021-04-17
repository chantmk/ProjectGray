using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPublisher
{
    // Player delegates
    //public delegate void OnPlayerJump();
    public delegate void OnPlayerPressFire();
    public delegate void OnPlayerFire();

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

    public static void TriggerPlayerFire()
    {
        PlayerFire?.Invoke();
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

    public delegate void OnStatusChange(BossStatus bossStatus);
    public static event OnStatusChange StatusChange;
    public static void TriggerStatus(BossStatus bossStatus)
    {
        StatusChange?.Invoke(bossStatus);
    }
}
