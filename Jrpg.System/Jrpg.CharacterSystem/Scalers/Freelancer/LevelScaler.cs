using System;
using System.Collections.Generic;

namespace Jrpg.CharacterSystem.Scalers.Freelancer
{
    public class LevelScaler : IStatisticScaler
    {
        private StatisticType type = StatisticType.Level;

        public Statistic NewStatistic(Dictionary<StatisticType, Statistic> statistic)
        {
            var copy = Copy(statistic[type]);

            copy.CurrentValue++;

            return copy;
        }

        public Statistic Copy(Statistic toCopy)
        {
            var statistic = new Statistic(type, toCopy.MaxValue);
            statistic.CurrentValue = toCopy.CurrentValue;

            return statistic;
        }
    }
}
