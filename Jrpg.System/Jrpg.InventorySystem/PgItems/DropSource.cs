using System.Collections.Generic;

namespace Jrpg.InventorySystem.PgItems
{
    public class DropSource
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public List<ItemClassEdge> ItemClass { get; set; }
    }
}
