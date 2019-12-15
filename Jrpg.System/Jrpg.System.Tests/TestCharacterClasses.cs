using System;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.InventorySystem;
using Jrpg.PartySystem;

using Xunit;

namespace Jrpg.System.Tests
{
    public class TestCharacterClasses
    {
        private MockGameLoop gameLoop;

        public TestCharacterClasses()
        {
            Console.WriteLine("---- Character Job Classes Test ----");
        }

        [Fact]
        public void TestWhiteMageShouldHaveRegen()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud", gameLoop.WhiteMage));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetMember("Cloud");
            Assert.Equal("White Mage", cloud.CurrentClassName());

            Assert.True(cloud.TechniqueDefinitions().Exists(t => t.Equals("Regen")));
        }

        [Fact]
        public void TestBlackmageShouldHaveFire()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud", gameLoop.BlackMage));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetMember("Cloud");
            Assert.Equal("Black Mage", cloud.CurrentClassName());

            Assert.True(cloud.TechniqueDefinitions().Exists(t => t.Equals("Fire")));
        }

        [Fact]
        public void TestSwitchingClasses()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud", gameLoop.WhiteMage));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetMember("Cloud");

            Assert.Equal("White Mage", cloud.CurrentClassName());
            Assert.True(cloud.TechniqueDefinitions().Exists(t => t.Equals("Regen")));
            Assert.False(cloud.TechniqueDefinitions().Exists(t => t.Equals("Fire")));

            cloud.ChangeClass(gameLoop.BlackMage);

            Assert.Equal("Black Mage", cloud.CurrentClassName());
            Assert.False(cloud.TechniqueDefinitions().Exists(t => t.Equals("Regen")));
            Assert.True(cloud.TechniqueDefinitions().Exists(t => t.Equals("Fire")));
        }
    }
}
