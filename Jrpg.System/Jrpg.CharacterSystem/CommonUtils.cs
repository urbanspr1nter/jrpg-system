using System;
using Jrpg.CharacterSystem.Classes.Definitions;
namespace Jrpg.CharacterSystem
{
    public class CommonUtils
    {
        public static StatisticType ToStatisticType(string label)
        {
            switch(label)
            {
                case "HP Current":
                    return StatisticType.HpCurrent;
                case "HP Max":
                    return StatisticType.HpMax;
                case "MP Current":
                    return StatisticType.MpCurrent;
                case "MP Max":
                    return StatisticType.MpMax;
                case "Experience":
                    return StatisticType.Experience;
                case "Strength":
                    return StatisticType.Strength;
                case "Speed":
                    return StatisticType.Speed;
                case "Stamina":
                    return StatisticType.Stamina;
                case "Magic":
                    return StatisticType.Magic;
                case "Attack":
                    return StatisticType.Attack;
                case "Defense":
                    return StatisticType.Defense;
                case "Evasion":
                    return StatisticType.Evasion;
                case "Magic Defense":
                    return StatisticType.MagicDefense;
                case "Magic Evasion":
                    return StatisticType.MagicEvasion;
                default:
                    return StatisticType.Level;
            }
        }

        public static Statistic ToStatistic(ClassStatistic stat)
        {
            var statisticType = ToStatisticType(stat.Name);
            var statistic = new Statistic(statisticType, StatisticTypeCollection.MaxValues[statisticType])
            {
                CurrentValue = stat.Value
            };

            return statistic;
        }
    }
}
