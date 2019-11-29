using System;
using System.Linq;
using System.Collections.Generic;
using Jrpg.InventorySystem.Utils.DbReader;
using Jrpg.InventorySystem.PgItems;
using Jrpg.CharacterSystem;

namespace Jrpg.System.Tests.Common
{
    public class MockedItemsDbReader : IDbReader
    {
        public List<Item> ReadData<Item>(string filename)
        {
            var diamondSwordEdge = new ItemClassEdge
            {
                Name = "Diamond Sword",
                Weight = 15
            };

            var sword1 = new Jrpg.InventorySystem.PgItems.Item
            {
                Name = "Sword 1",
                BodyPart = "Arms",
                ItemClass = new List<ItemClassEdge>() { diamondSwordEdge },
                Properties = new List<Property>(),
                Value = 0
            };

            var diamondSword = new Jrpg.InventorySystem.PgItems.Item
            {
                Name = "Diamond Sword",
                BodyPart = "Arms",
                ItemClass = new List<ItemClassEdge>(),
                Properties = new List<Property> {
                    new Property { Name = CharacterStatistics.LabelAttack, Value = 100 }
                },
                Value = 5
            };

            var list = new List<Jrpg.InventorySystem.PgItems.Item> {
                sword1,
                diamondSword
            };

            var dbFileContents = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(dbFileContents);
        }
    }
}
