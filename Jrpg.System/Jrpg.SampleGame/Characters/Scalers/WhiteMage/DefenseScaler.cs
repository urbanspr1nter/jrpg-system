using System;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem;
using System.Collections.Generic;

namespace Jrpg.SampleGame.Characters.Scalers.WhiteMage
{
    public class DefenseScaler : BaseScaler
    {
        public DefenseScaler()
        {
            type = StatisticType.Defense;
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
