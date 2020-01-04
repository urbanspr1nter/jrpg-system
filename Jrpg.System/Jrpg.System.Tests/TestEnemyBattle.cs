using System;
using Jrpg.CharacterSystem;
using Jrpg.InventorySystem;
using Jrpg.PartySystem;

using Xunit;

namespace Jrpg.System.Tests
{
    public class TestEnemyBattle
    {
        private MockGameLoop gameLoop;

        public TestEnemyBattle()
        {
            Console.WriteLine("---- Enemy Tests ----");
        }

        [Fact]
        public void TestGenerateEnemy()
        {
            gameLoop = new MockGameLoop();

            var enemy = gameLoop.EnemyManager.GetEnemyInstance("Goblin", "Basic Goblin");

            Assert.NotNull(enemy);
            Assert.Equal("Goblin", enemy.Name);
            Assert.Equal("Basic Goblin", enemy.CurrentClassName());
        }

        [Fact]
        public void TestGetGoldAndExperienceFromEnemy()
        {
            gameLoop = new MockGameLoop();

            var enemy = gameLoop.EnemyManager.GetEnemyInstance("Goblin", "Basic Goblin");

            Assert.Equal(25, enemy.Gold());
            Assert.Equal(10, enemy.Experience());
        }

        [Fact]
        public void TestEnemyAlive()
        {
            gameLoop = new MockGameLoop();

            var enemy = gameLoop.EnemyManager.GetEnemyInstance("Goblin", "Basic Goblin");

            Assert.True(enemy.Statistics[StatisticType.HpCurrent].CurrentValue > 0);
            Assert.True(enemy.IsAlive());

            enemy.Statistics[StatisticType.HpCurrent].CurrentValue = 0;
            Assert.False(enemy.IsAlive());
        }

        [Fact]
        public void TestEnemyDropItem()
        {
            gameLoop = new MockGameLoop();

            var enemy = gameLoop.EnemyManager.GetEnemyInstance("Goblin", "Basic Goblin");
            var item = enemy.GetItem();

            Assert.NotNull(item);
            Assert.True(item.Name.Equals("Tonic") || item.Name.Equals("Antidote"));
        }

        [Fact]
        public void TestBattle()
        {
            gameLoop = new MockGameLoop();

            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            party.AddMember("Cloud", new Character("Cloud", gameLoop.BlackMage));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetActiveCharacter();
            var enemy = gameLoop.EnemyManager.GetEnemyInstance("Goblin", "Basic Goblin");

            gameLoop.SetGameState(GameState.GameStateValue.Battle);

            int i = 0;
            while(enemy.IsAlive())
            {
                enemy.Statistics[StatisticType.HpCurrent].CurrentValue -= GetDamageToDeal(cloud);
                gameLoop.Step();
                i++;

                if (i > enemy.Statistics[StatisticType.HpMax].CurrentValue)
                    Assert.True(false);
            }

            Assert.False(enemy.IsAlive());

        }

        private int GetDamageToDeal(Character active)
        {
            return active.Statistics[StatisticType.Attack].CurrentValue * 2;
        }
    }
}
