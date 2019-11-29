using System;
using System.Collections.Generic;
using Xunit;
using Jrpg.System.Tests.Common;
using Jrpg.InventorySystem.Utils.DbReader;
using Jrpg.InventorySystem.PgItems;
using Jrpg.InventorySystem;
using Jrpg.InventorySystem.Items;
using Jrpg.CharacterSystem;
using Jrpg.PartySystem;

namespace Jrpg.System.Tests
{
    public class TestKeyItemUsage
    {
        private static Dictionary<string, object> GlobalGameState = new Dictionary<string, object>();

        private class SuperSecureDoor : IItemSubscriber
        {
            public bool Locked { get; private set; } = true;

            private string LockStatus()
            {
                return Locked ? "LOCKED" : "UNLOCKED";
            }

            public void Publish(Dictionary<string, object> message)
            {
                Console.WriteLine($"The door is currently {LockStatus()}");
                Console.WriteLine("Attempting to unlock...");

                if (Locked)
                    Locked = (bool)message["result"];

                Console.WriteLine($"... The door is {LockStatus()}!");
            }
        }

        private class SodaPopDrinker : IItemSubscriber
        {
            public void Publish(Dictionary<string, object> message)
            {
                GlobalGameState["GuardOnDuty"] = (bool)message["result"];
            }
        }

        private class Guard : Character {
            public Guard(string name) : base(name)
            {
            }

            public bool DutyStatus()
            {
                if ((bool)GlobalGameState["GuardOnDuty"])
                {
                    Console.WriteLine("I'm too thirsty to move...");
                }
                else
                {
                    Console.WriteLine("Ah, that hits the spot. Oh you need to get in? Sure, I'll move!");
                }

                return (bool)GlobalGameState["GuardOnDuty"];
            }
        }

        private InventoryManager inventoryManager;
        private Party party;
        private TestUtilities testUtils;
        private IDbReader itemsDbReader;

        public TestKeyItemUsage()
        {
            GlobalGameState["GuardOnDuty"] = true;
        }

        [Fact]
        public void TestCardKey()
        {
            Console.WriteLine("--- Card Key Test --- ");

            itemsDbReader = new ItemsDbReader();
            testUtils = new TestUtilities(itemsDbReader.ReadData<Item>(DbConstants.ItemsDbFile));

            inventoryManager = new InventoryManager();
            party = new Party(inventoryManager);

            Character terra = new Character("Terra");
            party.AddMember(terra.Name, terra);

            var cardKey = testUtils.CardKeyItem();
            var subscriber = new SuperSecureDoor();

            Assert.True(subscriber.Locked);

            cardKey.Register(subscriber);

            party.ReceiveItem(cardKey, ItemReceiveAction.Reward);
            party.SetActiveCharacter(terra.Name);

            party.UseItem(TestUtilities.CardKey, terra);

            Assert.False(subscriber.Locked);
        }

        [Fact]
        public void TestSodaPop()
        {
            Console.WriteLine("--- Soda Pop Test --- ");

            itemsDbReader = new ItemsDbReader();
            testUtils = new TestUtilities(itemsDbReader.ReadData<Item>(DbConstants.ItemsDbFile));

            inventoryManager = new InventoryManager();
            party = new Party(inventoryManager);

            Character terra = new Character("Terra");
            party.AddMember(terra.Name, terra);

            Guard guard = new Guard("Guard");

            var sodaPop = testUtils.SodaPopItem();
            var subscriber = new SodaPopDrinker();

            Assert.True(guard.DutyStatus());

            sodaPop.Register(subscriber);

            party.ReceiveItem(sodaPop, ItemReceiveAction.Purchase);
            Assert.Equal(1, party.QueryFor(TestUtilities.SodaPop).Quantity);

            party.SetActiveCharacter(terra.Name);
            party.UseItem(TestUtilities.SodaPop, guard);

            Console.WriteLine("You gave a drink to the thirsty guard on duty.");
            Assert.False(guard.DutyStatus());

            Assert.Equal(0, party.QueryFor(TestUtilities.SodaPop).Quantity);
        }
    }
}
