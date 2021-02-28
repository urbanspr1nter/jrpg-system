using System.Collections.Generic;

namespace Jrpg.GameState
{
    public class GameStateSubscriber : IGameStateSubscriber
    {
        public Dictionary<string, object> Context { get; }

        public GameStateSubscriber(Dictionary<string, object> context)
        {
            Context = context;
        }

        public virtual void Receive(GameStateValue state)
        {
            // not implemented
        }
        public virtual void OnRegister()
        {
            // No-op
        }

        public virtual void OnUnregister()
        {
            // No-op
        }
    }
}
