using System.Collections.Generic;
namespace Jrpg.InventorySystem.Utils.DbReader
{
    public interface IDbReader
    {
        List<T> ReadData<T>(string filename);
    }
}
