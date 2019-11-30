using System;
using Jrpg.GameState;
namespace Jrpg.CharacterSystem.StatusEffects.Effects
{
     class Regen : StatusEffect
    {
        public override void PerformEffect(Character character, GameStateValue state)
        {
            if (state != GameStateValue.Battle)
            {
                return;
            }

            character.Statistics[StatisticType.HpCurrent].CurrentValue += 2;
        }

        public override bool ShouldDestroy(Character character, GameStateValue state)
        {
            if(state == GameStateValue.Battle)
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
