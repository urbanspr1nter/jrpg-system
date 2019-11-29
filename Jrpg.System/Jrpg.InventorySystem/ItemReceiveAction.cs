using System.ComponentModel;
namespace Jrpg.InventorySystem
{
    public enum ItemReceiveAction
    {
        [Description("Treasure")]
        Treasure,

        [Description("Loot")]
        Loot,

        [Description("Purchase")]
        Purchase,

        [Description("Reward")]
        Reward
    }
}
