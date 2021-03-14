using System;
using System.Collections.Generic;
using System.Linq;
using Jrpg.CharacterSystem.Classes;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.CharacterSystem.Statistics;

namespace Jrpg.CharacterSystem
{
    public abstract class Character
    {
        public CharacterBody Body { get; }
        public string Name { get; set; }
        public StatisticRegistry Statistics {
            get
            {
                return CurrentClass.Statistics;
            }
            set
            {
                CurrentClass.Statistics = value;
            }
        }

        public List<string> TechniqueDefinitions { 
            get
            {
                return CurrentClass.TechniqueDefinitions.Select(tech => tech.DisplayName).ToList();
            } 
        }

        public string CurrentClassName
        {
            get
            {
                return CurrentClass.ClassName();
            }
        }

        protected BaseCharacterClass CurrentClass;

        private int _nextExpLimit;
        public int ExperienceForNextLevel {
            get {
                return _nextExpLimit;
            }
        }

        public Character(string name, BaseCharacterClass defaultJobClass)
        {
            Body = new CharacterBody();
            Name = name;
            CurrentClass = defaultJobClass;

            var startingStats = new StatisticRegistry();
            var AllTypes = Statistics.StatisticTypes();
            foreach(var statType in AllTypes)
            {
                var defaultJobClassStatistic = defaultJobClass.Statistics.GetCopy(statType);
                startingStats.Add(statType, new Statistic(statType, defaultJobClassStatistic.DefaultValue, defaultJobClassStatistic.MaxValue, new BaseStatisticModifier()));
            }
        }

        protected abstract bool MaybeLevelUp();

        public abstract bool IsAlive();

        public abstract bool CanUseTechnique(string techniqueName);

        public abstract void UseTechnique(TechniqueName name, StatusEffectManager statusEffectManager, List<Character> targets);

        public abstract bool AddExperience(int experience);

        public abstract void ChangeClass(BaseCharacterClass jobClass);

        private int NextExperienceThreshold(int currentLevel)
        {
            // Inspired by Gen1 Pokemon's experience curve.
            return (int)((4 * Math.Pow(currentLevel, 3)) / 5);
        }
    }
}
