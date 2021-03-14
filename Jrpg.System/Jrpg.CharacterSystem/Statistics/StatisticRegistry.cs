using System;
using System.Collections.Generic;
using System.Text;

namespace Jrpg.CharacterSystem.Statistics
{
    public class StatisticRegistry
    {
        private Dictionary<StatisticType, Statistic> registry;

        public StatisticRegistry()
        {
            registry = new Dictionary<StatisticType, Statistic>();
        }

        public bool Add(StatisticType type, Statistic value)
        {
            if (registry.ContainsKey(type))
            {
                return false;
            }

            registry.Add(type, value);

            return true;
        }

        public bool Remove(StatisticType type)
        {
            if (!registry.ContainsKey(type))
            {
                return false;
            }

            registry.Remove(type);

            return true;
        }

        public bool Up(StatisticType type)
        {
            if (!registry.ContainsKey(type))
            {
                return false;
            }

            registry[type].Up();

            return true;
        }

        public bool Down(StatisticType type)
        {
            if (!registry.ContainsKey(type))
            {
                return false;
            }

            registry[type].Down();

            return true;
        }

        public Statistic GetCopy(StatisticType type)
        {
            if (!registry.ContainsKey(type))
            {
                return null;
            }

            var statistic = registry[type].Clone();

            return statistic;
        }

        public List<StatisticType> StatisticTypes()
        {
            return new List<StatisticType>(registry.Keys);
        }
    }
}
