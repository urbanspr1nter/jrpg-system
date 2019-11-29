using System;
using Jrpg.CharacterSystem.StatusEffects.Effects;

namespace Jrpg.CharacterSystem.StatusEffects
{
    class StatusEffectFactory
    {
        public static StatusEffect BuildStatusEffect(StatusEffectType statusEffectType)
        {
            if (statusEffectType == StatusEffectType.Mini)
            {
                return new Mini();
            }
            else if (statusEffectType == StatusEffectType.Poison)
            {
                return new Poison();
            }
            else if (statusEffectType == StatusEffectType.Regen)
            {
                return new Regen();
            }

            return null;
        }
    }
}
