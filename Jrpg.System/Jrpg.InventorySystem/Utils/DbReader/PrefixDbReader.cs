using System.Collections.Generic;
using System.IO;

namespace Jrpg.InventorySystem.Utils.DbReader
{
    public class PrefixDbReader : IDbReader
    {
        public List<Affix> ReadData<Affix>(string filename)
        {
            var contents = File.ReadAllText($"{filename}");
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Affix>>(contents);

            return data;
        }
    }
}
