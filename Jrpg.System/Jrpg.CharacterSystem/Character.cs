using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.Classes;

namespace Jrpg.CharacterSystem
{
    public class Character
    {
        public CharacterBody Body { get; }
        public string Name { get; set; }
        public Dictionary<StatisticType, Statistic> Statistics {
            get;
            set;
        }

        private Freelancer currentClass;

        private int _nextExpLimit;
        public int ExperienceForNextLevel {
            get {
                return _nextExpLimit;
            }
        }

        public Character(string name)
        {
            Body = new CharacterBody();
            Name = name;
            Statistics = new Dictionary<StatisticType, Statistic>();
            currentClass = new Freelancer(Statistics);

            var StatMaxes = StatisticTypeCollection.MaxValues;
            var DefaultValues = StatisticTypeCollection.DefaultValues;
            foreach(var statType in StatisticTypeCollection.All)
            {
                Statistics.Add(statType, new Statistic(statType, StatMaxes[statType]));
                Statistics[statType].CurrentValue = DefaultValues[statType];
            }
        }

        private int NextExperienceThreshold(int currentLevel)
        {
            // Inspired by Gen1 Pokemon's experience curve.
            return (int)((4 * Math.Pow(currentLevel, 3)) / 5);
        }

        public bool AddExperience(int experience)
        {
            Statistics[StatisticType.Experience].CurrentValue += experience;

            bool result = false;
            while (MaybeLevelUp() == true)
            {
                result = true;
            }

            return result;
        }

        public string CurrentClassName()
        {
            return currentClass.ClassName();
        }

        private bool MaybeLevelUp()
        {
            if (Statistics[StatisticType.Experience].CurrentValue < _nextExpLimit)
            {
                return false;
            }

            var currentLevel = Statistics[StatisticType.Level].CurrentValue;
            _nextExpLimit = NextExperienceThreshold(currentLevel);

            currentClass.LevelUp();

            return true;
        }
    }
}
