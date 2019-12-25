/**
 * Represents a basic Enemy class. All monsters are Character types with this
 * specific job class as the default job class.
 */ 
using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.Classes.Definitions;
using Jrpg.CharacterSystem.Classes;

namespace Jrpg.SampleGame.Characters.JobClasses
{
    public class Enemy : BaseCharacterClass
    {
        public Enemy(Dictionary<StatisticType, Statistic> statistics,
            List<TechniqueDefinition> techniqueDefinitions,
            List<ClassTechniqueDefinition> techniqueDefinitionMapping)
            : base(statistics, techniqueDefinitions, techniqueDefinitionMapping)
        {
        }

        public override string ClassName()
        {
            return "Enemy";
        }
    }
}
