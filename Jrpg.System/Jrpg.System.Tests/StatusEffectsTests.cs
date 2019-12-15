using System;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.GameState;
using Jrpg.InventorySystem;
using Jrpg.PartySystem;
using Xunit;

namespace Jrpg.System.Tests
{
    public class StatusEffectsTests
    {
        private MockGameLoop gameLoop;


        [Fact]
        public void TestPoisonEffect()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);
            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud"));
            party.AddMember("Tifa", new Character("Tifa"));
            party.AddMember("Aerith", new Character("Aerith"));

            var cloud = party.GetMember("Cloud");
            var tifa = party.GetMember("Tifa");
            var aerith = party.GetMember("Aerith");

            gameLoop.SetGameState(GameStateValue.Battle);

            gameLoop.StatusEffectManager.ApplyEffect(tifa, "Poison");
            Assert.Equal(30, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.Step();
            Assert.Equal(28, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.SetGameState(GameStateValue.World);

            gameLoop.Step();
            Assert.Equal(26, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.Step();
            Assert.Equal(24, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);
        }

        [Fact]
        public void TestRegenEffect()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);
            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud"));
            party.AddMember("Tifa", new Character("Tifa"));
            party.AddMember("Aerith", new Character("Aerith"));

            var cloud = party.GetMember("Cloud");
            var tifa = party.GetMember("Tifa");
            var aerith = party.GetMember("Aerith");

            gameLoop.SetGameState(GameStateValue.Battle);

            aerith.Statistics[StatisticType.HpCurrent].CurrentValue = 20;

            gameLoop.StatusEffectManager.ApplyEffect(aerith, "Regen");
            Assert.Equal(30, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(20, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.Step();

            Assert.Equal(1, gameLoop.StatusEffectManager.StatusEffectNames(aerith).Count);

            Assert.Equal(30, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(22, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.Step();
            Assert.Equal(30, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(24, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.SetGameState(GameStateValue.World);
            gameLoop.Step();

            Assert.Equal(0, gameLoop.StatusEffectManager.StatusEffectNames(aerith).Count);

            gameLoop.Step();
            gameLoop.Step();

            Assert.Equal(30, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(24, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);

        }

        [Fact]
        public void TestMiniEffect()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);
            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud"));
            party.AddMember("Tifa", new Character("Tifa"));
            party.AddMember("Aerith", new Character("Aerith"));

            var cloud = party.GetMember("Cloud");
            var tifa = party.GetMember("Tifa");
            var aerith = party.GetMember("Aerith");

            cloud.AddExperience(100);
            tifa.AddExperience(100);
            aerith.AddExperience(100);

            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);

            Assert.Equal(7, cloud.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, tifa.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, aerith.Statistics[StatisticType.Attack].CurrentValue);

            gameLoop.SetGameState(GameStateValue.Battle);
            gameLoop.StatusEffectManager.ApplyEffect(cloud, "Mini");
            gameLoop.Step();

            Assert.Equal(30, tifa.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, aerith.Statistics[StatisticType.HpCurrent].CurrentValue);

            Assert.Equal(1, cloud.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, tifa.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, aerith.Statistics[StatisticType.Attack].CurrentValue);

            gameLoop.SetGameState(GameStateValue.World);
            gameLoop.Step();
            Assert.Equal(7, cloud.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, tifa.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, aerith.Statistics[StatisticType.Attack].CurrentValue);

            gameLoop.SetGameState(GameStateValue.Battle);
            gameLoop.Step();
            Assert.Equal(1, cloud.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, tifa.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, aerith.Statistics[StatisticType.Attack].CurrentValue);

            gameLoop.Step();
            gameLoop.StatusEffectManager.RemoveEffect(cloud, "Mini");
            gameLoop.Step();
            Assert.Equal(7, cloud.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, tifa.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(7, aerith.Statistics[StatisticType.Attack].CurrentValue);
        }

    }
}
