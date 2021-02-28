using System;
using System.Collections.Generic;
namespace Jrpg.GameState
{
    public class GameStateManager : IGameStatePublisher
    {
        private readonly List<IGameStateSubscriber> subscribers;
        public GameStateValue State { get; private set; }

        public GameStateManager(GameStateValue initialState)
        {
            subscribers = new List<IGameStateSubscriber>();
            State = initialState;
        }

        public void Transition(GameStateValue state)
        {
            if (subscribers.Count == 0)
            {
                return;
            }

            State = state;
            foreach(var subscriber in subscribers)
            {
                subscriber.Receive(state);
            }
        }

        public void Register(IGameStateSubscriber subscriber)
        {
            if(subscribers.Contains(subscriber))
            {
                return;
            }

            subscriber.OnRegister();
            
            subscribers.Add(subscriber);
        }

        public void Unregister(IGameStateSubscriber subscriber)
        {
            if(!subscribers.Contains(subscriber))
            {
                return;
            }

            subscriber.OnUnregister();
            
            subscribers.Remove(subscriber);
        }
    }
}
