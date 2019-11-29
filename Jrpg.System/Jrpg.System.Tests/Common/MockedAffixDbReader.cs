using System.Collections.Generic;
using Jrpg.InventorySystem;
using Jrpg.InventorySystem.Utils.DbReader;
using Jrpg.InventorySystem.PgItems;
using Jrpg.CharacterSystem;

namespace Jrpg.System.Tests.Common
{
    public class MockedAffixDbReader : IDbReader
    {
        public List<Affix> ReadData<Affix>(string filename)
        {
            var affix = new Jrpg.InventorySystem.PgItems.Affix();
            affix.Value = new ValueObject();
            affix.Weight = 15;
            affix.ParentItemClass = new List<string> { "Sword 1" };

            if (filename.Equals("Prefixes"))
            {
                affix.Name = "Prismatic";
                affix.Properties = new List<Property> {
                    new Property() {
                        Name = CharacterStatistics.LabelMagic,
                        Value = 67
                    }
                };
            }
            else
            {
                affix.Name = "of the Sun";
                affix.Properties = new List<Property> {
                    new Property() {
                        Name = CharacterStatistics.LabelSpeed,
                        Value = 33
                    }
                };
            }

            var result = new List<Jrpg.InventorySystem.PgItems.Affix>();
            result.Add(affix);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Affix>>(
                Newtonsoft.Json.JsonConvert.SerializeObject(result)
            );
        }
    }
}
