using System;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem;
using System.Collections.Generic;

namespace Jrpg.SampleGame.Characters.Scalers.WhiteMage
{
    public class MagicScaler : BaseScaler
    {
        public MagicScaler()
        {
            type = StatisticType.Magic;
        }

        public override Statistic NewStatistic(Dictionary<StatisticType, Statistic> statistic)
        {
            var offset = new Random().Next(2, 3);

            var copy = Copy(statistic[type]);

            copy.CurrentValue += offset;

            return copy;
        }
    }
}
