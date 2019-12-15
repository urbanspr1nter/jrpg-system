using System;
using System.Collections.Generic;
namespace Jrpg.CharacterSystem.Classes.Definitions
{
    public class ClassDefinition
    {
        public string Name { get; set; }
        public string Agent { get; set; }
        public List<ClassTechniqueDefinition> Techniques;
        public List<ClassStatistic> StartingStatistics;
    }
}
