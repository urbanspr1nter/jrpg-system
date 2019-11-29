using System;
using Jrpg.CharacterSystem.GameState;
using System.Collections.Generic;

namespace Jrpg.CharacterSystem.StatusEffects.Effects
{
    class Mini : StatusEffect
    {
        private Dictionary<StatisticType, Statistic> currentStats;
        private Dictionary<StatisticType, Statistic> miniStats;

        public override void OnApply(Character character, GameStateValue state)
        {
            currentStats = new Dictionary<StatisticType, Statistic>(character.Statistics);
            miniStats = new Dictionary<StatisticType, Statistic>();
            foreach (var stat in StatisticTypeCollection.DefaultValues.Keys)
            {
                if (stat == StatisticType.HpCurrent || stat == StatisticType.HpMax ||
                    stat == StatisticType.MpCurrent || stat == StatisticType.MpMax ||
                    stat == StatisticType.Level || stat == StatisticType.Experience)
                {
                    miniStats[stat] = character.Statistics[stat];
                    continue;
                }

                Statistic miniStat = new Statistic(stat, 1);
                miniStat.CurrentValue = 1;

                miniStats.Add(stat, miniStat);
            }
        }

        public override void PerformEffect(Character character, GameStateValue state)
        {
            if(state == GameStateValue.Battle)
            {
                character.Statistics = miniStats;
            } else
            {
                character.Statistics = currentStats;
            }
        }

        public override void OnRemove(Character character, GameStateValue state)
        {
            character.Statistics = currentStats;
        }

        public override StatusEffectType GetStatusEffectType()
        {
            return StatusEffectType.Mini;
        }
    }
}
