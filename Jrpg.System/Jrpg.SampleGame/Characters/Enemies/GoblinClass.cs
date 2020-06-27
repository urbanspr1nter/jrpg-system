using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Classes.Definitions;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.InventorySystem.PgItems;
using Jrpg.BattleSystem.Enemies;

namespace Jrpg.SampleGame.Characters.Enemies
{
    public class GoblinClass : EnemyClass
    {
        public GoblinClass(
            Dictionary<StatisticType, Statistic> statistics,
            List<TechniqueDefinition> techniqueDefinitions,
            List<ClassTechniqueDefinition> techniqueDefinitionMapping,
            List<ItemClassEdge> itemClasses,
            Proximity proximity,
            int gold,
            int experience
        ) : base(statistics, techniqueDefinitions, techniqueDefinitionMapping, itemClasses, proximity, gold, experience)
        {

        }

        public override string ClassName()
        {
            return "Basic Goblin";
        }
    }
}
