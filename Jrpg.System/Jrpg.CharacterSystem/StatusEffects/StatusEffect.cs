using System;
using Jrpg.GameState;
namespace Jrpg.CharacterSystem.StatusEffects
{
    abstract class StatusEffect: IStatusEffect
    {
        public virtual void AfterEffect(Character character, GameStateValue state)
        {
            return;
        }

        public virtual void BeforeEffect(Character character, GameStateValue state)
        {
            return;
        }

        public virtual void OnApply(Character character, GameStateValue state)
        {
            return;
        }

        public virtual void OnRemove(Character character, GameStateValue state)
        {
            return;
        }

        public virtual void PerformEffect(Character character, GameStateValue state)
        {
            return;
        }

        public virtual bool ShouldDestroy(Character character, GameStateValue state)
        {
            return false;
        }

        public abstract StatusEffectType GetStatusEffectType();
    }
}
