using System.Collections.Generic;

namespace Jrpg.CharacterSystem.Statistics
{
    public class StatisticTypeRegistry
    {
        private readonly Dictionary<string, StatisticType> registry;

        public int Count
        {
            get
            {
                return registry.Count;
            }
        }

        public StatisticTypeRegistry() {
            registry = new Dictionary<string, StatisticType>();
        }

        public bool Add(StatisticType value)
        {
            if (registry.ContainsKey(value.Name))
            {
                return false;
            }

            registry.Add(value.Name, value);

            return true;
        }

        public bool Remove(StatisticType value)
        {
            if (registry.Count == 0 || registry.ContainsKey(value.Name))
            {
                return false;
            }

            registry.Remove(value.Name);

            return false;
        }


        public StatisticType FindByName(string name)
        {
            if (registry.Count == 0 || registry.ContainsKey(name))
            {
                return null;
            }

            return registry[name];
        }

        public List<StatisticType> List()
        {
            return new List<StatisticType>(registry.Values);
        }
    }
}
