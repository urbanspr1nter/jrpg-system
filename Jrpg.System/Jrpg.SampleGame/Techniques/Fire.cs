using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.StatusEffects;

namespace Jrpg.SampleGame.Techniques
{
    public class Fire : Technique
    {
        public Fire(StatusEffectManager statusEffectManager, TechniqueDefinition definition)
            : base(statusEffectManager, definition)
        {
        }

        public override void Perform(Character source, List<Character> targets)
        {
            var totalDamage = Definition.MagicPower * (Math.Pow(source.Statistics[StatisticType.Magic].CurrentValue, 2) / 6 + Definition.MagicPower) / 4;
            var damage = Convert.ToInt32(totalDamage / targets.Count);
            
            foreach(var target in targets)
            {
                target.Statistics[StatisticType.HpCurrent].CurrentValue -= damage;
            }

            source.Statistics[StatisticType.MpCurrent].CurrentValue -= Definition.MpCost;
        }
    }
}
