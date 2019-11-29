using System;
namespace Jrpg.CharacterSystem.GameState
{
    public interface IGameStateSubscriber
    {
        void ReceiveStateUpdate(GameStateValue state);
    }
}
