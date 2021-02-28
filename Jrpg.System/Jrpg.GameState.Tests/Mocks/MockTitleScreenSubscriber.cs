using System;
using System.Collections.Generic;
using System.Text;

namespace Jrpg.GameState.Tests.Mocks
{
    class MockTitleScreenSubscriber : GameStateSubscriber
    {
        public MockTitleScreenSubscriber(Dictionary<string, object> context) : base(context)
        {
        }

        public override void Receive(GameStateValue state)
        {
            if (state.Name.Equals("Title"))
            {
                Context["state.title.test"] = "1";
            }
        }

        public override void OnRegister()
        {
            Context.Add("state.title.test", "0");
        }

        public override void OnUnregister()
        {
            Context.Remove("state.title.test");
        }
    }
}
