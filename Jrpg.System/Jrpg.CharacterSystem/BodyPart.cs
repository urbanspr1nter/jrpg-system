using System.ComponentModel;

namespace Jrpg.CharacterSystem
{
    public enum BodyPart
    {
        [Description("Default")]
        Default,

        [Description("Head")]
        Head,

        [Description("Chest")]
        Chest,

        [Description("Legs")]
        Legs,

        [Description("Arms")]
        Arms,

        [Description("Hands")]
        Hands,

        [Description("Accessory")]
        Accessory
    }
}
