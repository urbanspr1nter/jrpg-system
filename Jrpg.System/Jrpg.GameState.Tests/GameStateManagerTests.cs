using System.Collections.Generic;
using NUnit.Framework;

namespace Jrpg.GameState.Tests
{
    public class Tests
    {
        GameStateManager manager;
        Dictionary<string, object> context;

        // Create all the states
        readonly GameStateValue titleState = GameStateValue.Create("Title", 0);
        readonly GameStateValue worldState = GameStateValue.Create("World", 1);

        GameStateSubscriber titleStateSubscriber;
        GameStateSubscriber worldStateSubscriber;

        [SetUp]
        public void Setup()
        {
            context = new Dictionary<string, object>();
            titleStateSubscriber = new Mocks.MockTitleScreenSubscriber(context);
            worldStateSubscriber = new Mocks.MockWorldSubscriber(context);
            manager = new GameStateManager(titleState);
            manager.Register(titleStateSubscriber);
        }

        [Test]
        public void TestInitializeToTitleState()
        {
            Assert.AreEqual(manager.State.Name, "Title");
        }

        [Test]
        public void TestOnRegister()
        {
            Assert.IsFalse(context.ContainsKey("state.world.test"));

            manager.Register(worldStateSubscriber);

            Assert.AreEqual(context["state.world.test"], "0");
        }

        [Test]
        public void TestStateTransition()
        {
            Assert.IsFalse(context.ContainsKey("state.world.test"));

            manager.Register(worldStateSubscriber);
            manager.Transition(worldState);

            Assert.AreEqual(context["state.world.test"], "1");
            Assert.AreEqual(manager.State.Name, worldState.Name);

            manager.Transition(titleState);

            Assert.AreEqual(manager.State.Name, titleState.Name);
            Assert.AreEqual(context["state.title.test"], "1");
        }

        [Test]
        public void TestOnUnRegister()
        {
            manager.Register(worldStateSubscriber);
            Assert.IsTrue(context.ContainsKey("state.world.test"));

            manager.Unregister(worldStateSubscriber);
            Assert.IsFalse(context.ContainsKey("state.world.test"));
        }
    }
}