using System;
namespace Jrpg.GameState
{
    public interface IGameStateSubscriber
    {
        void Receive(GameStateValue state);

        void OnRegister();

        void OnUnregister();
    }
}
