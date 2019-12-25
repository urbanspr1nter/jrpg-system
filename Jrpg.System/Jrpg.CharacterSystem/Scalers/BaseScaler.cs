using System;
using System.Collections.Generic;

namespace Jrpg.CharacterSystem.Scalers
{
    public abstract class BaseScaler : IStatisticScaler
    {
        protected StatisticType type;

        public virtual Statistic Copy(Statistic toCopy)
        {
            var statistic = new Statistic(type, toCopy.MaxValue);
            statistic.CurrentValue = toCopy.CurrentValue;

            return statistic;
        }

        public virtual Statistic NewStatistic(Dictionary<StatisticType, Statistic> statistic)
        {
            var offset = 1;
            var copy = Copy(statistic[type]);

            copy.CurrentValue += offset;

            return copy;
        }
    }
}
