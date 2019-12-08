using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.GameState;
using Jrpg.InventorySystem;
using Jrpg.PartySystem;
using Xunit;

namespace Jrpg.System.Tests
{
    public class TestTechniques
    {
        private MockGameLoop gameLoop;

        public TestTechniques()
        {
            Console.WriteLine("---- Techniques Test ----");
        }

        [Fact]
        public void TestRegenTechnique()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud"));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetMember("Cloud");

            cloud.AddExperience(100);

            gameLoop.SetGameState(GameStateValue.Battle);
            Assert.Equal(30, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
            cloud.UseTechnique(TechniqueName.Regen, gameLoop.StatusEffectManager, new List<Character> { party.GetActiveCharacter() });
            Assert.True(gameLoop.StatusEffectManager.StatusEffectTypes(cloud).Exists(effect => effect == StatusEffectType.Regen));

            gameLoop.Step();
            Assert.True(gameLoop.StatusEffectManager.StatusEffectTypes(cloud).Exists(effect => effect == StatusEffectType.Regen));
            Assert.Equal(32, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.Step();
            Assert.True(gameLoop.StatusEffectManager.StatusEffectTypes(cloud).Exists(effect => effect == StatusEffectType.Regen));
            Assert.Equal(34, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.Step();
            Assert.False(gameLoop.StatusEffectManager.StatusEffectTypes(cloud).Exists(effect => effect == StatusEffectType.Regen));
            Assert.Equal(36, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.Step();
            Assert.False(gameLoop.StatusEffectManager.StatusEffectTypes(cloud).Exists(effect => effect == StatusEffectType.Regen));
            Assert.Equal(36, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);

            gameLoop.Step();
            Assert.False(gameLoop.StatusEffectManager.StatusEffectTypes(cloud).Exists(effect => effect == StatusEffectType.Regen));
            Assert.Equal(36, cloud.Statistics[StatisticType.HpCurrent].CurrentValue);
        }

        [Fact]
        public void TestFireTechnique()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud"));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetMember("Cloud");
            cloud.AddExperience(100);

            var goblin = new Character("Goblin");
            goblin.AddExperience(50);
            goblin.Statistics[StatisticType.HpCurrent].CurrentValue = goblin.Statistics[StatisticType.HpMax].CurrentValue;

            Assert.Equal(goblin.Statistics[StatisticType.HpMax].CurrentValue, goblin.Statistics[StatisticType.HpCurrent].CurrentValue);

            var enemies = new List<Character> { goblin };

            gameLoop.SetGameState(GameStateValue.Battle);

            cloud.UseTechnique(TechniqueName.Fire, gameLoop.StatusEffectManager, enemies);

            var damage = Math.Abs(goblin.Statistics[StatisticType.HpMax].CurrentValue - goblin.Statistics[StatisticType.HpCurrent].CurrentValue);

            Console.WriteLine("Fire did " + damage + " damage");

            Assert.InRange(damage, 0, 20);
        }

        [Fact]
        public void TestFiraTechnique()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud"));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetMember("Cloud");
            cloud.AddExperience(100);

            var goblin = new Character("Goblin");
            goblin.AddExperience(50);
            goblin.Statistics[StatisticType.HpCurrent].CurrentValue = goblin.Statistics[StatisticType.HpMax].CurrentValue;

            Assert.Equal(goblin.Statistics[StatisticType.HpMax].CurrentValue, goblin.Statistics[StatisticType.HpCurrent].CurrentValue);

            var enemies = new List<Character> { goblin };

            gameLoop.SetGameState(GameStateValue.Battle);

            cloud.UseTechnique(TechniqueName.Fira, gameLoop.StatusEffectManager, enemies);

            var damage = Math.Abs(goblin.Statistics[StatisticType.HpMax].CurrentValue - goblin.Statistics[StatisticType.HpCurrent].CurrentValue);

            Console.WriteLine("Fira did " + damage + " damage");

            Assert.InRange(damage, 50, 100);
        }

        [Fact]
        public void TestFiragaTechnique()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud"));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetMember("Cloud");
            cloud.AddExperience(100);

            var goblin = new Character("Goblin");
            goblin.AddExperience(50);
            goblin.Statistics[StatisticType.HpCurrent].CurrentValue = goblin.Statistics[StatisticType.HpMax].CurrentValue;

            Assert.Equal(goblin.Statistics[StatisticType.HpMax].CurrentValue, goblin.Statistics[StatisticType.HpCurrent].CurrentValue);

            var enemies = new List<Character> { goblin };

            gameLoop.SetGameState(GameStateValue.Battle);

            cloud.UseTechnique(TechniqueName.Firaga, gameLoop.StatusEffectManager, enemies);

            var damage = Math.Abs(goblin.Statistics[StatisticType.HpMax].CurrentValue - goblin.Statistics[StatisticType.HpCurrent].CurrentValue);

            Console.WriteLine("Firaga did " + damage + " damage");

            Assert.InRange(damage, 500, 1000);
        }
    }
}
