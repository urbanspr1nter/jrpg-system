using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.Classes.Definitions;
using Jrpg.CharacterSystem.Techniques;

namespace Jrpg.CharacterSystem.Classes
{
    public abstract class BaseCharacterClass : ICharacterClass
    {
        public Dictionary<StatisticType, Statistic> Statistics { get; set; }
        public List<ClassTechniqueDefinition> TechniqueDefinitionMapping { get; private set; }
        public List<TechniqueDefinition> TechniqueDefinitions { get; private set; }

        public BaseCharacterClass(Dictionary<StatisticType, Statistic> statistics,
            List<TechniqueDefinition> techniqueDefinitions,
            List<ClassTechniqueDefinition> techniqueDefinitionMapping
        )
        {
            Statistics = statistics;
            TechniqueDefinitions = techniqueDefinitions;
            TechniqueDefinitionMapping = techniqueDefinitionMapping;
        }

        public virtual void LevelUp()
        {
            Statistics[StatisticType.Level] = NextLevel();
            Statistics[StatisticType.HpMax] = NextHpMax();
            Statistics[StatisticType.MpMax] = NextMpMax();
            Statistics[StatisticType.Strength] = NextStrength();
            Statistics[StatisticType.Speed] = NextSpeed();
            Statistics[StatisticType.Stamina] = NextStamina();
            Statistics[StatisticType.Magic] = NextMagic();
            Statistics[StatisticType.Attack] = NextAttack();
            Statistics[StatisticType.Defense] = NextDefense();
            Statistics[StatisticType.Evasion] = NextEvasion();
            Statistics[StatisticType.MagicDefense] = NextMagicDefense();
            Statistics[StatisticType.MagicEvasion] = NextMagicEvasion();
        }

        public virtual void AddTechnique(TechniqueDefinition techniqueDefinition)
        {
            TechniqueDefinitions.Add(techniqueDefinition);
        }

        public virtual Statistic NextLevel()
        {
            var statistic = new Statistic(StatisticType.Level, StatisticTypeCollection.MaxValues[StatisticType.Level]);

            statistic.CurrentValue = Statistics[StatisticType.Level].CurrentValue + 1;

            return statistic;
        }

        public virtual Statistic NextHpMax()
        {
            var statistic = new Statistic(StatisticType.HpMax, StatisticTypeCollection.MaxValues[StatisticType.HpMax]);

            statistic.CurrentValue = Statistics[StatisticType.HpMax].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextMpMax()
        {
            var statistic = new Statistic(StatisticType.MpMax, StatisticTypeCollection.MaxValues[StatisticType.MpMax]);

            statistic.CurrentValue = Statistics[StatisticType.MpMax].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextStrength()
        {
            var statistic = new Statistic(StatisticType.Strength, StatisticTypeCollection.MaxValues[StatisticType.Strength]);

            statistic.CurrentValue = Statistics[StatisticType.Strength].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextSpeed()
        {
            var statistic = new Statistic(StatisticType.Speed, StatisticTypeCollection.MaxValues[StatisticType.Speed]);

            statistic.CurrentValue = Statistics[StatisticType.Speed].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextStamina()
        {
            var statistic = new Statistic(StatisticType.Stamina, StatisticTypeCollection.MaxValues[StatisticType.Stamina]);

            statistic.CurrentValue = Statistics[StatisticType.Stamina].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextMagic()
        {
            var statistic = new Statistic(StatisticType.Magic, StatisticTypeCollection.MaxValues[StatisticType.Magic]);

            statistic.CurrentValue = Statistics[StatisticType.Magic].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextAttack()
        {
            var statistic = new Statistic(StatisticType.Attack, StatisticTypeCollection.MaxValues[StatisticType.Attack]);

            statistic.CurrentValue = Statistics[StatisticType.Attack].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextDefense()
        {
            var statistic = new Statistic(StatisticType.Defense, StatisticTypeCollection.MaxValues[StatisticType.Defense]);

            statistic.CurrentValue = Statistics[StatisticType.Defense].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextEvasion()
        {
            var statistic = new Statistic(StatisticType.Evasion, StatisticTypeCollection.MaxValues[StatisticType.Evasion]);

            statistic.CurrentValue = Statistics[StatisticType.Evasion].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextMagicDefense()
        {
            var statistic = new Statistic(StatisticType.MagicDefense, StatisticTypeCollection.MaxValues[StatisticType.MagicDefense]);

            statistic.CurrentValue = Statistics[StatisticType.MagicDefense].CurrentValue;

            return statistic;
        }

        public virtual Statistic NextMagicEvasion()
        {
            var statistic = new Statistic(StatisticType.MagicEvasion, StatisticTypeCollection.MaxValues[StatisticType.MagicEvasion]);

            statistic.CurrentValue = Statistics[StatisticType.MagicEvasion].CurrentValue;

            return statistic;
        }

        public abstract string ClassName();
    }
}
