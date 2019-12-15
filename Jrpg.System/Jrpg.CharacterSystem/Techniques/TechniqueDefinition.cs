using System;
namespace Jrpg.CharacterSystem.Techniques
{
    public class TechniqueDefinition
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public int MpCost { get; set; }
        public int AttackPower { get; set; }
        public int MagicPower { get; set; }
        public string Agent { get; set; }
    }
}
