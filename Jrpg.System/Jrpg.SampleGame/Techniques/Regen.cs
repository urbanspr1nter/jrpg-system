using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.StatusEffects;

namespace Jrpg.SampleGame.Techniques
{
    public class Regen : Technique
    {
        public Regen(StatusEffectManager statusEffectManager, TechniqueDefinition definition)
            : base(statusEffectManager, definition)
        {
        }

        public override void Perform(Character source, List<Character> targets)
        {
            foreach(var target in targets)
            {
                StatusEffectManager.ApplyEffect(target, "Regen");
            }

            source.Statistics[StatisticType.MpCurrent].CurrentValue -= Definition.MpCost;
        }
    }
}
