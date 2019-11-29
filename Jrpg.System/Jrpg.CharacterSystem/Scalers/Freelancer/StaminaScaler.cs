﻿using System;
using System.Collections.Generic;

namespace Jrpg.CharacterSystem.Scalers.Freelancer
{
    public class StaminaScaler : IStatisticScaler
    {
        private StatisticType type = StatisticType.Stamina;

        public Statistic Copy(Statistic toCopy)
        {
            var statistic = new Statistic(type, toCopy.MaxValue);
            statistic.CurrentValue = toCopy.CurrentValue;

            return statistic;
        }

        public Statistic NewStatistic(Dictionary<StatisticType, Statistic> statistic)
        {
            var offset = 1;
            var copy = Copy(statistic[type]);

            copy.CurrentValue += offset;

            return copy;
        }
    }
}
