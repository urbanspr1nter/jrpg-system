using System;
using Jrpg.CharacterSystem;
using Xunit;

namespace Jrpg.System.Tests
{
    public class CharacterTests
    {
        [Fact]
        public void CreatesCharacter()
        {
            Character hero = new Character("Hero");

            Assert.NotNull(hero);
            Assert.Equal("Hero", hero.Name);
            Assert.Equal(1, hero.Statistics[StatisticType.Level].CurrentValue);
        }

        [Fact]
        public void CharacterLevelsUp()
        {
            Character hero = new Character("Hero");
            Assert.Equal(1, hero.Statistics[StatisticType.Level].CurrentValue);
            Assert.Equal(1, hero.Statistics[StatisticType.Strength].CurrentValue);

            var hasLeveledUp = hero.AddExperience(100);

            Assert.True(hasLeveledUp);
            Assert.Equal(7, hero.Statistics[StatisticType.Level].CurrentValue);
            Assert.Equal(7, hero.Statistics[StatisticType.Strength].CurrentValue);

            Assert.Equal(172, hero.ExperienceForNextLevel);
        }

        [Fact]
        public void CharacterHasDefaultClass()
        {
            Character hero = new Character("Hero");
            Assert.Equal("Freelancer", hero.CurrentClassName());
        }
    }
}
