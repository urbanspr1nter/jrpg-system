using System;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.GameState;

namespace Jrpg.SampleGame.StatusEffects
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

        public override string GetStatusEffectName()
        {
            return "Poison";
        }
    }
}
