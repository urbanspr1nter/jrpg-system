using System.Collections.Generic;
using Jrpg.GameState;

namespace Jrpg.GameState.Tests.Mocks
{
    class MockWorldSubscriber : GameStateSubscriber
    {
        public MockWorldSubscriber(Dictionary<string, object> context) : base(context) { }

        public override void Receive(GameStateValue state)
        {
            if (state.Name.Equals("World"))
            {
                Context["state.world.test"] = "1";
            }
        }

        public override void OnRegister()
        {
            Context.Add("state.world.test", "0");
        }

        public override void OnUnregister()
        {
            Context.Remove("state.world.test");
        }
    }
}
