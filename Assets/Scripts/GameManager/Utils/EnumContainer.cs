﻿namespace Utils
{
    public enum MovementEnum
    {
        Idle,
        Move,
        Roll,
        Shock
    }
    public enum StatusEnum
    {
        Immortal,
        Mortal,
        Dead
    }

    public enum BossAggroEnum
    {
        Calm,
        Enrage,
        Hyper,
        LastStand
    }

    public enum BossAttackEnum
    {
        Normal,
        Enrage,
        Hyper
    }

    public enum DecisionEnum
    {
        Wait,
        Mercy,
        Kill
    }

    public enum DialogueStateEnum
    {
        Enter,
        LastStand,
        Decision,
        Kill,
        Mercy
    }
    
    public enum EnvStateEnum
    {
        ZeroCharge,
        Charging,
        Discharging,
        Activataed,
    }
}
