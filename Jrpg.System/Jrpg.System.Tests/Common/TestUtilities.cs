using System;
using System.Collections.Generic;
using Jrpg.InventorySystem;
using Jrpg.InventorySystem.Items;
using Jrpg.InventorySystem.PgItems;
using Jrpg.ItemComponents;

namespace Jrpg.System.Tests.Common
{
    public class TestUtilities
    {
        public static ItemName Tonic = new ItemName("Tonic");
        public static ItemName LeatherHelmet = new ItemName("Leather Helmet");
        public static ItemName IronHelmet = new ItemName("Iron Helmet");
        public static ItemName CardKey = new ItemName("Card Key");
        public static ItemName SodaPop = new ItemName("Soda Pop");

        private List<Item> items;

        public TestUtilities(List<Item> items)
        {
            this.items = items;
        }

        public BaseItem TonicItem()
        {
            var tonicItemDef = items.Find(i => i.Name.Equals(Tonic.ToString()));

            return new BaseItem(tonicItemDef);
        }

        public BaseItem LeatherHelmetItem()
        {
            var leatherHelmetItemDef = items.Find(i => i.Name.Equals(LeatherHelmet.ToString()));

            return new BaseItem(leatherHelmetItemDef);
        }

        public BaseItem IronHelmetItem()
        {
            var ironHelmetItemDef = items.Find(i => i.Name.Equals(IronHelmet.ToString()));

            return new BaseItem(ironHelmetItemDef);
        }

        public BaseItem CardKeyItem()
        {
            var cardKeyItemDef = items.Find(i => i.Name.Equals(CardKey.ToString()));

            return new BaseItem(cardKeyItemDef);
        }

        public BaseItem SodaPopItem()
        {
            var sodaPopItemDef = items.Find(i => i.Name.Equals(SodaPop.ToString()));

            return new BaseItem(sodaPopItemDef);
        }

        public void SeedInventoryManager(InventoryManager inventoryManager)
        {
            inventoryManager.Acquire(TonicItem());
            inventoryManager.Acquire(TonicItem());
            inventoryManager.Acquire(TonicItem());
            inventoryManager.Acquire(LeatherHelmetItem());
            inventoryManager.Acquire(LeatherHelmetItem());
            inventoryManager.Acquire(IronHelmetItem());
            inventoryManager.Acquire(CardKeyItem());
        }
    }
}
