using System;
using System.Collections.Generic;
using System.Linq;
using Jrpg.CharacterSystem.Classes;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.StatusEffects;

namespace Jrpg.CharacterSystem
{
    public class Character
    {
        public CharacterBody Body { get; }
        public string Name { get; set; }
        public Dictionary<StatisticType, Statistic> Statistics {
            get
            {
                return currentClass.Statistics;
            }
            set
            {
                currentClass.Statistics = value;
            }
        }

        private BaseCharacterClass currentClass;

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
            var startingStats = new Dictionary<StatisticType, Statistic>();

            var StatMaxes = StatisticTypeCollection.MaxValues;
            var DefaultValues = StatisticTypeCollection.DefaultValues;
            foreach(var statType in StatisticTypeCollection.All)
            {
                startingStats.Add(statType, new Statistic(statType, StatMaxes[statType]));
                startingStats[statType].CurrentValue = DefaultValues[statType];
            }

            currentClass = new Freelancer(startingStats,
                new List<TechniqueDefinition>(),
                new List<Classes.Definitions.ClassTechniqueDefinition>());
        }

        public Character(string name, BaseCharacterClass defaultJobClass)
        {
            Body = new CharacterBody();
            Name = name;
            currentClass = defaultJobClass;
        }

        public bool CanUseTechnique(string techniqueName)
        {
            var techniqueDef = currentClass.TechniqueDefinitionMapping.Find(def => def.Name.Equals(techniqueName));

            if (techniqueDef == null || techniqueDef.Level > Statistics[StatisticType.Level].CurrentValue)
                return false;
            return true;
        }

        public void UseTechnique(TechniqueName name, StatusEffectManager statusEffectManager, List<Character> targets)
        {
            var techniqueDefinition = currentClass.TechniqueDefinitions.Find(t => t.DisplayName.Equals(name.Name));

            if (techniqueDefinition == null)
            {
                return;
            }

            var technique = new TechniqueFactory(statusEffectManager).CreateTechnique(techniqueDefinition);

            technique.Perform(this, targets);
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

        public void ChangeClass(BaseCharacterClass jobClass)
        {
            var currentLevel = currentClass.Statistics[StatisticType.Level].CurrentValue;
            var currentExperience = currentClass.Statistics[StatisticType.Experience].CurrentValue;
            var currentHp = currentClass.Statistics[StatisticType.HpCurrent].CurrentValue;
            var currentMp = currentClass.Statistics[StatisticType.MpCurrent].CurrentValue;

            currentClass = jobClass;

            for (var i = 1; i  < currentLevel; i++)
            {
                currentClass.LevelUp();
            }

            if(currentHp > currentClass.Statistics[StatisticType.HpMax].CurrentValue)
            {
                currentClass.Statistics[StatisticType.HpCurrent].CurrentValue =
                    currentClass.Statistics[StatisticType.HpMax].CurrentValue;
            }

            if(currentMp > currentClass.Statistics[StatisticType.MpMax].CurrentValue)
            {
                currentClass.Statistics[StatisticType.MpCurrent].CurrentValue =
                    currentClass.Statistics[StatisticType.MpMax].CurrentValue;
            }

            currentClass.Statistics[StatisticType.HpCurrent].CurrentValue = currentHp;
            currentClass.Statistics[StatisticType.MpCurrent].CurrentValue = currentMp;
            currentClass.Statistics[StatisticType.Experience].CurrentValue = currentExperience;
        }

        public string CurrentClassName()
        {
            return currentClass.ClassName();
        }

        public List<string> TechniqueDefinitions()
        {
            return currentClass.TechniqueDefinitions.Select(t => t.DisplayName).ToList();
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
