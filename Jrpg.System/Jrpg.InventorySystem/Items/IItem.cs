using System.Collections.Generic;
using Jrpg.CharacterSystem;

namespace Jrpg.InventorySystem
{
    public interface IItem
    {
        bool CanApply(Character targetChar);
        bool Apply(Character targetChar, Dictionary<string, object> keyItemParamters = null);
        bool UndoApply(Character targetChar);
    }
}
