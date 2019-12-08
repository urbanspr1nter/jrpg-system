using System;
using Jrpg.GameState;
namespace Jrpg.CharacterSystem.StatusEffects.Effects
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

        public override StatusEffectType GetStatusEffectType()
        {
            return StatusEffectType.Regen;
        }
    }
}
