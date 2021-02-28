using System;
namespace Jrpg.GameState
{
    public interface IGameStatePublisher
    {
        void Register(IGameStateSubscriber subscriber);
        void Unregister(IGameStateSubscriber subscriber);
        void Transition(GameStateValue state);
        GameStateValue State { get; }
    }
}
