using System;
using System.Collections.Generic;
namespace Jrpg.CharacterSystem.GameState
{
    public class GameStateManager : IGameStatePublisher
    {
        private List<IGameStateSubscriber> subscribers;
        private GameStateValue currentState;

        public GameStateManager(GameStateValue initialState)
        {
            subscribers = new List<IGameStateSubscriber>();
            currentState = initialState;
        }

        public void PublishStateUpdate(GameStateValue state)
        {
            currentState = state;
            foreach(var subscriber in subscribers)
            {
                subscriber.ReceiveStateUpdate(state);
            }
        }

        public void Register(IGameStateSubscriber subscriber)
        {
            if(subscribers.Contains(subscriber))
            {
                return;
            }

            subscribers.Add(subscriber);
        }

        public void Unregister(IGameStateSubscriber subscriber)
        {
            if(!subscribers.Contains(subscriber))
            {
                return;
            }
            subscribers.Remove(subscriber);
        }

        public GameStateValue CurrentState()
        {
            return currentState;
        }
    }
}
