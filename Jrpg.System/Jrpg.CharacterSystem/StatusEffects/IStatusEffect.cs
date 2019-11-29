using System;
using Jrpg.CharacterSystem.GameState;
namespace Jrpg.CharacterSystem.StatusEffects
{
    interface IStatusEffect
    {
        void OnApply(Character character, GameStateValue state);
        void BeforeEffect(Character character, GameStateValue state);
        void PerformEffect(Character character, GameStateValue state);
        void AfterEffect(Character character, GameStateValue state);
        void OnRemove(Character character, GameStateValue state);
        bool ShouldDestroy(Character character, GameStateValue state);
    }
}
