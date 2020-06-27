using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.Classes.Definitions;
using Jrpg.InventorySystem.PgItems;

namespace Jrpg.BattleSystem.Enemies.Definitions
{
    public class EnemyDefinition : ClassDefinition
    {
        public string Id { get; set; }
        public string Elemental { get; set; }
        public List<ItemClassEdge> ItemClass { get; set; }
        public Proximity Proximity { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }

        public EnemyDefinition()
        {
            ItemClass = new List<ItemClassEdge>();
            Proximity = new Proximity();
        }
    }
}
