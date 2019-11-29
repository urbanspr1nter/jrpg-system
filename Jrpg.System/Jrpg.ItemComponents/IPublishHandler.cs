using System.Collections.Generic;
namespace Jrpg.ItemComponents
{
    public interface IPublishHandler
    {
        Dictionary<string, object> GetMessage(Dictionary<string, object> parameters);
    }
}
