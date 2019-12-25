using System;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem;
using System.Collections.Generic;

namespace Jrpg.SampleGame.Characters.Scalers.BlackMage
{
    public class MagicEvasionScaler : BaseScaler
    {
        public MagicEvasionScaler()
        {
            type = StatisticType.MagicEvasion;
        }

        public override Statistic NewStatistic(Dictionary<StatisticType, Statistic> statistic)
        {
            var offset = new Random().Next(2, 4);

            var copy = Copy(statistic[type]);

            copy.CurrentValue += offset;

            return copy;
        }
    }
}
