using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Classes;

namespace Jrpg.SampleGame.Characters.JobClasses
{
    public class WhiteMage : BaseCharacterClass
    {
        public WhiteMage(Dictionary<StatisticType, Statistic> statistics): base(statistics)
        {
        }

        public override string ClassName()
        {
            return "White Mage";
        }
    }
}
