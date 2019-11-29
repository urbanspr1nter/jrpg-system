using System.Collections.Generic;
namespace Jrpg.InventorySystem.PgItems
{
    public class Affix
    {
        public class AffixLabel
        {
            public static string NoPrefix = "NoPrefix";
            public static string NoSuffix = "NoSuffix";
        }

        public string Name { get; set; }
        public List<string> ParentItemClass { get; set; }
        public int Weight { get; set; }
        public List<Property> Properties { get; set; }
        public ValueObject Value { get; set; }
    }
}
