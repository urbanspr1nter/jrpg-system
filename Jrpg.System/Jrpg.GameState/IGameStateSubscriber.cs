using System;
namespace Jrpg.GameState
{
    public interface IGameStateSubscriber
    {
        void ReceiveStateUpdate(GameStateValue state);
    }
}
