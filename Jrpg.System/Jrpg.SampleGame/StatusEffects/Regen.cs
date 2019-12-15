using System;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.GameState;

namespace Jrpg.SampleGame.StatusEffects
{
     class Regen : StatusEffect
    {
        private int turns;

        public override void OnApply(Character character, GameStateValue state)
        {
            turns = 0;
        }

        public override void PerformEffect(Character character, GameStateValue state)
        {
            if (state != GameStateValue.Battle)
            {
                return;
            }

            character.Statistics[StatisticType.HpCurrent].CurrentValue += 2;

            turns++;
        }

        public override bool ShouldDestroy(Character character, GameStateValue state)
        {
            if(state == GameStateValue.Battle && turns < 3)
            {
                return false;
            }

            return true;
        }

        public override string GetStatusEffectName()
        {
            return "Regen";
        }
    }
}
