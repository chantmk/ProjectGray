using System.Collections.Generic;

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

    public enum CharacterNameEnum
    {
        Player,
        Black,
        Blue,
        Yellow,
        Red,
        Purple,
        Green,
        Orange,
        White
    }

    public static class CharacterName
    {

        private static Dictionary<CharacterNameEnum, string> nameMap = new Dictionary<CharacterNameEnum, string>()
        {
            { CharacterNameEnum.Player, "ทายาทน้อย" },
            { CharacterNameEnum.Black, "แบล็ค" },
            { CharacterNameEnum.Blue, "บลู" },
            { CharacterNameEnum.Yellow, "เบลโล่" },
        };

        public static string GetName(CharacterNameEnum nameEnum)
        {
            return nameMap[nameEnum];
        }
    }
}