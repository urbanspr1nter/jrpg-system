using System.Collections.Generic;
using Jrpg.ItemComponents;

namespace Jrpg.Items.PublishHandlers
{
    public class DrinkHandler : IPublishHandler
    {
        public Dictionary<string, object> GetMessage(Dictionary<string, object> parameters)
        {
            return new Dictionary<string, object> {
                { "result", false }
            };
        }
    }
}
