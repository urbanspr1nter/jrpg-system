using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.StatusEffects;

namespace Jrpg.CharacterSystem.Techniques.Concrete
{
    public class Regen : Technique
    {
        public Regen(StatusEffectManager statusEffectManager) : base(statusEffectManager)
        {
            Id = "Tech_Regen";
            DisplayName = "Regen";
            MpCost = 18;
            AttackPower = 0;
            MagicPower = 0;
        }

        public override void Perform(Character source, List<Character> targets)
        {
            foreach(var target in targets)
            {
                StatusEffectManager.ApplyEffect(target, StatusEffectType.Regen);
            }

            source.Statistics[StatisticType.MpCurrent].CurrentValue -= MpCost;
        }
    }
}
