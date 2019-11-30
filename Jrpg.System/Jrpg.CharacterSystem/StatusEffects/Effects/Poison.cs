using System;
using Jrpg.GameState;
namespace Jrpg.CharacterSystem.StatusEffects.Effects
{
    class Poison : StatusEffect
    {
        public override void PerformEffect(Character character, GameStateValue state)
        {
            if(state == GameStateValue.Menu)
            {
                return;
            }

            character.Statistics[StatisticType.HpCurrent].CurrentValue -= 2;
        }

        public override StatusEffectType GetStatusEffectType()
        {
            return StatusEffectType.Poison;
        }
    }
}
