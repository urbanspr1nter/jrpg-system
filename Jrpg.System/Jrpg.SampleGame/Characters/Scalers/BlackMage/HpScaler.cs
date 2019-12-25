using System;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem;
using System.Collections.Generic;

namespace Jrpg.SampleGame.Characters.Scalers.BlackMage
{
    public class HpScaler : BaseScaler
    {
        public HpScaler()
        {
            type = StatisticType.HpMax;
        }

        public override Statistic NewStatistic(Dictionary<StatisticType, Statistic> statistic)
        {
            var offset = new Random().Next(1, 3);

            var copy = Copy(statistic[type]);

            copy.CurrentValue += offset;

            return copy;
        }
    }
}
