using System;
using System.Collections.Generic;

namespace Jrpg.CharacterSystem.Scalers.Freelancer
{
    public class MpScaler : IStatisticScaler
    {
        private StatisticType type = StatisticType.MpMax;

        public Statistic NewStatistic(Dictionary<StatisticType, Statistic> statistic)
        {
            // Grow MP randomly from 1-2 MP at a time
            var offset = new Random().Next(1, 3);
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
