using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.ItemComponents;
using Jrpg.InventorySystem.Items;

namespace Jrpg.InventorySystem
{
    public class InventoryManager
    {
        private Dictionary<string, ItemData> registry;

        public InventoryManager()
        {
            registry = new Dictionary<string, ItemData>();
        }

        public List<ItemInfo> QueryAll()
        {
            List<ItemInfo> data = new List<ItemInfo>();

            foreach(var name in registry.Keys)
            {
                data.Add(new ItemInfo
                {
                    Quantity = registry[name].Quantity,
                    Name = new ItemName(name),
                    Value = registry[name].Item.Value,
                    Description = name,
                    IsKeyItem = registry[name].Item.IsKeyItem
                }) ;
            }

            return data;
        }

        public bool Use(ItemName name, Character targetChar, Dictionary<string, object> keyParameters = null)
        {
            var key = name.ToString();

            if (registry[key].Quantity <= 0)
            {
                return false;
            }

            var item = registry[key].Item;

            if(!item.CanApply(targetChar))
            {
                return false;
            }

            item.Apply(targetChar, keyParameters);

            registry[key].Quantity--;

            return true;
        }

        public bool Drop(ItemName name)
        {
            var key = name.ToString();

            if (registry[key].Quantity <= 0)
            {
                return false;
            }

            registry[key].Quantity--;
            return true;
        }

        public bool Restore(ItemName name, Character targetChar)
        {
            var key = name.ToString();

            var item = registry[key].Item;
            registry[key].Quantity++;

            return item.UndoApply(targetChar);
        }

        public bool Acquire(BaseItem baseItem)
        {
            var key = baseItem.Name;

            if(!registry.ContainsKey(key))
            {
                registry.Add(key, new ItemData {
                    Quantity = 0,
                    Item = baseItem
                });
            }

            registry[key].Quantity++;

            return true;
        }

        public bool HasItem(ItemName name)
        {
            var key = name.ToString();

            return registry.ContainsKey(key) &&
                registry[key].Quantity > 0;
        }
    }
}
