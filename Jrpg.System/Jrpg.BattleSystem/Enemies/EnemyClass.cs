using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Classes.Definitions;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.Classes;
using Jrpg.InventorySystem.PgItems;

namespace Jrpg.BattleSystem.Enemies
{
    public abstract class EnemyClass : BaseCharacterClass
    {
        public int Gold { get; private set; }
        public int Experience { get; private set; }
        public List<ItemClassEdge> ItemClasses { get; private set; }

        public EnemyClass(
            Dictionary<StatisticType, Statistic> statistics,
            List<TechniqueDefinition> techniqueDefinitions,
            List<ClassTechniqueDefinition> techniqueDefinitionMapping,
            List<ItemClassEdge> itemClasses,
            int gold,
            int experience
        )
            : base(statistics, techniqueDefinitions, techniqueDefinitionMapping)
        {
            Gold = gold;
            Experience = experience;
            ItemClasses = itemClasses;
        }


        public override abstract string ClassName();
    }
}
