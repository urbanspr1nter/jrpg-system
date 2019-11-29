using System;
namespace Jrpg.InventorySystem.PgItems
{
    public class ItemClassEdge
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public override bool Equals(object obj)
        {
            var that = (ItemClassEdge)obj;
            return that.Name.Equals(Name) && that.Weight == Weight;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Weight);
        }
    }
}
