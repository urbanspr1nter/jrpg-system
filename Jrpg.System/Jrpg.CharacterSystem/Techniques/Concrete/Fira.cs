﻿using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.StatusEffects;

namespace Jrpg.CharacterSystem.Techniques.Concrete
{
    public class Fira : Technique
    {
        public Fira(StatusEffectManager statusEffectManager) : base(statusEffectManager)
        {
            Id = "Tech_Fira";
            DisplayName = "Fira";
            MpCost = 15;
            AttackPower = 0;
            MagicPower = 15;
        }

        public override void Perform(Character source, List<Character> targets)
        {
            var totalDamage = MagicPower * (Math.Pow(source.Statistics[StatisticType.Magic].CurrentValue, 2) / 6 + MagicPower) / 4;
            var damage = Convert.ToInt32(totalDamage / targets.Count);

            foreach (var target in targets)
            {
                target.Statistics[StatisticType.HpCurrent].CurrentValue -= damage;
            }

            source.Statistics[StatisticType.MpCurrent].CurrentValue -= MpCost;
        }
    }
}
