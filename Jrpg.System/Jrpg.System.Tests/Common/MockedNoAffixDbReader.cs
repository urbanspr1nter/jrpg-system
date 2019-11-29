
using System.Collections.Generic;
using Jrpg.InventorySystem.Utils.DbReader;
using Jrpg.InventorySystem.PgItems;

namespace Jrpg.System.Tests.Common
{
    public class MockedNoAffixDbReader : IDbReader
    {
        public List<Affix> ReadData<Affix>(string filename)
        {
            var affix = new Jrpg.InventorySystem.PgItems.Affix();
            affix.Value = new ValueObject();
            affix.Weight = 15;
            affix.Properties = new List<Property>();
            affix.ParentItemClass = new List<string> { "Sword 1" };

            if (filename.Equals("Prefixes"))
            {
                affix.Name = Jrpg.InventorySystem.PgItems.Affix.AffixLabel.NoPrefix;
            }
            else
            {
                affix.Name = Jrpg.InventorySystem.PgItems.Affix.AffixLabel.NoSuffix;
            }

            var result = new List<Jrpg.InventorySystem.PgItems.Affix>();
            result.Add(affix);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Affix>>(
                Newtonsoft.Json.JsonConvert.SerializeObject(result)
            );
        }
    }
}
