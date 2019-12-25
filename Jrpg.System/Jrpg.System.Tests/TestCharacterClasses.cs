using System;
using System.Linq;
using System.Collections.Generic;
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

        [Fact]
        public void TestSwitchingClassesWithStatCheck()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud"));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetMember("Cloud");

            Assert.Equal("Freelancer", cloud.CurrentClassName());

            while(cloud.Statistics[StatisticType.Level].CurrentValue < 11)
            {
                cloud.AddExperience(100);
            }

            PrintStatistics(cloud);

            var flLevel = cloud.Statistics[StatisticType.Level].CurrentValue;
            var flExperience = cloud.Statistics[StatisticType.Experience].CurrentValue;
            var flCurrentHp = cloud.Statistics[StatisticType.HpCurrent].CurrentValue;
            var flCurrentMp = cloud.Statistics[StatisticType.MpCurrent].CurrentValue;

            cloud.ChangeClass(gameLoop.WhiteMage);

            var wmSpeed = cloud.Statistics[StatisticType.Speed].CurrentValue;
            var wmStamina = cloud.Statistics[StatisticType.Stamina].CurrentValue;
            var wmMagic = cloud.Statistics[StatisticType.Magic].CurrentValue;
            var wmDefense = cloud.Statistics[StatisticType.Defense].CurrentValue;
            var wmEvasion = cloud.Statistics[StatisticType.Evasion].CurrentValue;
            var wmMagicDefense = cloud.Statistics[StatisticType.MagicDefense].CurrentValue;
            var wmMagicEvasion = cloud.Statistics[StatisticType.MagicEvasion].CurrentValue;
            var wmLevel = cloud.Statistics[StatisticType.Level].CurrentValue;
            var wmExperience = cloud.Statistics[StatisticType.Experience].CurrentValue;
            var wmCurrenHp = cloud.Statistics[StatisticType.HpCurrent].CurrentValue;
            var wmCurrentMp = cloud.Statistics[StatisticType.MpCurrent].CurrentValue;

            PrintStatistics(cloud);

            cloud.ChangeClass(gameLoop.BlackMage);

            var bmSpeed = cloud.Statistics[StatisticType.Speed].CurrentValue;
            var bmStamina = cloud.Statistics[StatisticType.Stamina].CurrentValue;
            var bmMagic = cloud.Statistics[StatisticType.Magic].CurrentValue;
            var bmDefense = cloud.Statistics[StatisticType.Defense].CurrentValue;
            var bmEvasion = cloud.Statistics[StatisticType.Evasion].CurrentValue;
            var bmMagicDefense = cloud.Statistics[StatisticType.MagicDefense].CurrentValue;
            var bmMagicEvasion = cloud.Statistics[StatisticType.MagicEvasion].CurrentValue;
            var bmLevel = cloud.Statistics[StatisticType.Level].CurrentValue;
            var bmExperience = cloud.Statistics[StatisticType.Experience].CurrentValue;
            var bmCurrentHp = cloud.Statistics[StatisticType.HpCurrent].CurrentValue;
            var bmCurrentMp = cloud.Statistics[StatisticType.MpCurrent].CurrentValue;

            PrintStatistics(cloud);

            Assert.Equal(flLevel, wmLevel);
            Assert.Equal(flLevel, bmLevel);
            Assert.Equal(flCurrentHp, wmCurrenHp);
            Assert.Equal(flCurrentHp, bmCurrentHp);
            Assert.Equal(flCurrentMp, wmCurrentMp);
            Assert.Equal(flCurrentMp, bmCurrentMp);
            Assert.Equal(flExperience, wmExperience);
            Assert.Equal(flExperience, bmExperience);

            // White Mages should have higher defensive/stamina stats
            // Black Mages should have higher offensive/sped stats
            Assert.True(wmStamina > bmStamina);
            Assert.True(wmDefense > bmDefense);
            Assert.True(wmMagicDefense > bmMagicDefense);
            Assert.True(wmMagic < bmMagic);
            Assert.True(wmSpeed < bmSpeed);
            Assert.True(wmEvasion < bmEvasion);
            Assert.True(wmMagicEvasion < bmMagicEvasion);
        }

        [Fact]
        public void TestEnsureTechniquesAreAppropriateForClass()
        {
            InventoryManager inventoryManager = new InventoryManager();
            Party party = new Party(inventoryManager);

            gameLoop = new MockGameLoop();

            party.AddMember("Cloud", new Character("Cloud"));
            party.SetActiveCharacter("Cloud");

            var cloud = party.GetMember("Cloud");

            cloud.ChangeClass(gameLoop.WhiteMage);

            Assert.True(cloud.TechniqueDefinitions().Exists(t => t.Equals("Regen")));
            Assert.False(cloud.TechniqueDefinitions().Exists(t => t.Equals("Fire")));

            cloud.ChangeClass(gameLoop.BlackMage);

            Assert.False(cloud.TechniqueDefinitions().Exists(t => t.Equals("Regen")));
            Assert.True(cloud.TechniqueDefinitions().Exists(t => t.Equals("Fire")));
        }

        private void PrintStatistics(Character character)
        {
            Console.WriteLine($"{character.Name} - {character.CurrentClassName()} - Statistics");
            Console.WriteLine($"Level: {character.Statistics[StatisticType.Level].CurrentValue}");
            Console.WriteLine($"HP: {character.Statistics[StatisticType.HpCurrent].CurrentValue} / {character.Statistics[StatisticType.HpMax].CurrentValue}");
            Console.WriteLine($"MP: {character.Statistics[StatisticType.MpCurrent].CurrentValue} / {character.Statistics[StatisticType.MpMax].CurrentValue}");
            Console.WriteLine($"Strength: {character.Statistics[StatisticType.Strength].CurrentValue}");
            Console.WriteLine($"Speed: {character.Statistics[StatisticType.Speed].CurrentValue}");
            Console.WriteLine($"Stamina: {character.Statistics[StatisticType.Stamina].CurrentValue}");
            Console.WriteLine($"Magic: {character.Statistics[StatisticType.Magic].CurrentValue}");
            Console.WriteLine($"Attack: {character.Statistics[StatisticType.Attack].CurrentValue}");
            Console.WriteLine($"Defense: {character.Statistics[StatisticType.Defense].CurrentValue}");
            Console.WriteLine($"Evasion: {character.Statistics[StatisticType.Evasion].CurrentValue}");
            Console.WriteLine($"Magic Defense: {character.Statistics[StatisticType.MagicDefense].CurrentValue}");
            Console.WriteLine($"Magic Evasion: {character.Statistics[StatisticType.MagicEvasion].CurrentValue}");
            Console.WriteLine($"Experience: {character.Statistics[StatisticType.Experience].CurrentValue}");

        }
    }
}
