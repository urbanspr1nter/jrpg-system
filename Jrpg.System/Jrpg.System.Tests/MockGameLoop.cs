using System;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.GameState;

namespace Jrpg.System.Tests
{
    public class MockGameLoop
    {
        private GameStateManager gameStateManager;
        public StatusEffectManager StatusEffectManager { get;  }

        public MockGameLoop()
        {
            gameStateManager = new GameStateManager(GameStateValue.World);
            StatusEffectManager = new StatusEffectManager();

            gameStateManager.Register(StatusEffectManager);
        }

        public void SetGameState(GameStateValue state)
        {
            gameStateManager.PublishStateUpdate(state);
        }

        public GameStateValue GetGameState()
        {
            return gameStateManager.CurrentState();
        }

        public void Step()
        {
            StatusEffectManager.BeforeEffects();
            StatusEffectManager.PerformEffects();
            StatusEffectManager.AfterEffects();
            StatusEffectManager.CleanUp();
        }
    }
}
