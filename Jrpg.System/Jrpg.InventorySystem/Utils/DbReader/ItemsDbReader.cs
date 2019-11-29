using System.IO;
using System.Collections.Generic;

namespace Jrpg.InventorySystem.Utils.DbReader
{
    public class ItemsDbReader : IDbReader
    {
        public List<Item> ReadData<Item>(string filename)
        {
            var contents = File.ReadAllText($"{filename}");
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(contents);

            return data;
        }
    }
}
