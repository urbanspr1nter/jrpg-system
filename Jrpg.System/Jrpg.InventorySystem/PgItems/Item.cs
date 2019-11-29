using System.Linq;
using System.Collections.Generic;
using System;

namespace Jrpg.InventorySystem.PgItems
{
    public class Item
    {
        public string Name { get; set; }
        public bool IsKeyItem { get; set; }
        public string PublishHandler { get; set; }
        public List<ItemClassEdge> ItemClass { get; set; }
        public List<Property> Properties { get; set; }
        public double Value { get; set; }
        public string BodyPart { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as Item;

            if (that == null)
            {
                return false;
            }

            if (!that.Name.Equals(Name))
            {
                return false;
            }
            if (that.IsKeyItem != IsKeyItem)
            {
                return false;
            }

            if (that.PublishHandler != null && PublishHandler != null)
            {
                if (!that.PublishHandler.Equals(PublishHandler))
                {
                    return false;
                }
            } else if (that.PublishHandler != null || PublishHandler != null)
            {
                return false;
            }

            if (!that.Value.Equals(Value))
            {
                return false;
            }
            if (!that.BodyPart.Equals(BodyPart))
            {
                return false;
            }
            if (!that.ItemClass.SequenceEqual(ItemClass))
            {
                return false;
            }
            if (!that.Properties.SequenceEqual(Properties))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Name,
                IsKeyItem,
                PublishHandler,
                ItemClass,
                Properties,
                Value,
                BodyPart
            );
        }
    }
}
