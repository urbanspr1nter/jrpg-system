using System.Collections.Generic;
using System.IO;

namespace Jrpg.InventorySystem.Utils.DbReader
{
    public class DropsDbReader : IDbReader
    {
        public List<DropSource> ReadData<DropSource>(string filename)
        {
            var contents = File.ReadAllText($"{filename}");
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DropSource>>(contents);

            return data;
        }
    }
}
