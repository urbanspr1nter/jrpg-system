using System.Collections.Generic;
namespace Jrpg.InventorySystem.Items
{
    public interface IItemSubscriber
    {
        void Publish(Dictionary<string, object> message);
    }
}
