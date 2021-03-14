using System;
using System.Collections.Generic;
namespace Jrpg.CharacterSystem.Statistics
{
    public class StatisticType { 
        public int Value { get; set; }
        public string Name { get; set; }

        private StatisticType(int value, string name) {
            Value = value;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (!(obj is StatisticType))
            {
                return false;
            }

            StatisticType test = (StatisticType)(obj);
            return test.Value == Value && test.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Name);
        }

        public static StatisticType CreateStatisticType(int value, string name) {
            return new StatisticType(value, name);
        }
    }
}
