using System;
using System.Collections.Generic;
using Jrpg.System.Interfaces;
using Jrpg.CharacterSystem.Statistics;
using Jrpg.CharacterSystem.Classes.Definitions;
using Jrpg.CharacterSystem.Techniques;

namespace Jrpg.CharacterSystem.Classes
{
    public abstract class BaseCharacterClass : ICharacterClass
    {
        public List<ClassTechniqueDefinition> TechniqueDefinitionMapping { get; private set; }
        public List<TechniqueDefinition> TechniqueDefinitions { get; private set; }

        public StatisticRegistry Statistics;

        public BaseCharacterClass(StatisticRegistry statistics,
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
            var statTypes = Statistics.StatisticTypes();

            foreach(var type in statTypes)
            {
                Statistics.Up(type);
            }
        }

        public virtual void AddTechnique(TechniqueDefinition techniqueDefinition)
        {
            TechniqueDefinitions.Add(techniqueDefinition);
        }

        public abstract string ClassName();
    }
}
