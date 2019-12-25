using System.Collections.Generic;
namespace Jrpg.CharacterSystem
{
    public enum StatisticType
    {
        Level = 0,
        HpCurrent,
        HpMax,
        MpCurrent,
        MpMax,
        Strength,
        Speed,
        Stamina,
        Magic,
        Attack,
        Defense,
        Evasion,
        MagicDefense,
        MagicEvasion,
        Experience
    }

    public class StatisticTypeCollection
    {
        public static List<StatisticType> All = new List<StatisticType>
        {
            StatisticType.Level,
            StatisticType.HpCurrent,
            StatisticType.HpMax,
            StatisticType.MpCurrent,
            StatisticType.MpMax,
            StatisticType.Strength,
            StatisticType.Speed,
            StatisticType.Stamina,
            StatisticType.Magic,
            StatisticType.Attack,
            StatisticType.Defense,
            StatisticType.Evasion,
            StatisticType.MagicDefense,
            StatisticType.MagicEvasion,
            StatisticType.Experience
        };

        public static Dictionary<StatisticType, int> MaxValues = new Dictionary<StatisticType, int>
        {
            { StatisticType.Level, 100},
            { StatisticType.HpCurrent, 9999},
            { StatisticType.HpMax, 9999 },
            { StatisticType.MpCurrent, 999 },
            { StatisticType.MpMax, 999 },
            { StatisticType.Strength, 255 },
            { StatisticType.Speed, 255 },
            { StatisticType.Stamina, 255 },
            { StatisticType.Magic, 255 },
            { StatisticType.Attack, 255 },
            { StatisticType.Defense, 255 },
            { StatisticType.Evasion, 255 },
            { StatisticType.MagicDefense, 255 },
            { StatisticType.MagicEvasion, 255 },
            { StatisticType.Experience, 1000000 }
        };

        public static Dictionary<StatisticType, int> DefaultValues = new Dictionary<StatisticType, int>
        {
            { StatisticType.Level, 1},
            { StatisticType.HpCurrent, 30},
            { StatisticType.HpMax, 30 },
            { StatisticType.MpCurrent, 1 },
            { StatisticType.MpMax, 1 },
            { StatisticType.Strength, 1 },
            { StatisticType.Speed, 1 },
            { StatisticType.Stamina, 1 },
            { StatisticType.Magic, 1 },
            { StatisticType.Attack, 1 },
            { StatisticType.Defense, 1 },
            { StatisticType.Evasion, 1 },
            { StatisticType.MagicDefense, 1 },
            { StatisticType.MagicEvasion, 1 },
            { StatisticType.Experience, 0 }
        };
    }
}
