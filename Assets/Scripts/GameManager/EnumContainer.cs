
public static class GrayConstants
{
    public const float EPSILON = 0.001f;
    public const float MINIMUM_TIME = 0.01f;
}

namespace Utils
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
}
