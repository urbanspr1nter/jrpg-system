using System;
using System.Collections.Generic;

namespace Jrpg.CharacterSystem.Scalers.Freelancer
{
    public class HpScaler : IStatisticScaler
    {
        private StatisticType type = StatisticType.HpMax;
        public Statistic NewStatistic(Dictionary<StatisticType, Statistic> statistic)
        {
            // Grow HP randomly from 1-3 HP at a time
            var offset = new Random().Next(1, 4);
            var copy = Copy(statistic[type]);

            copy.CurrentValue += offset;

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
