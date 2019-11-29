namespace Jrpg.CharacterSystem
{
    public class Statistic
    {
        public string Name { get; set; }
        public StatisticType Type { get; }

        private int _currentValue;
        public int CurrentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                if (value > MaxValue)
                {
                    _currentValue = MaxValue;
                }
                else
                {
                    _currentValue = value;
                }
            }
        }

        public int MaxValue { get; }
        
        public Statistic(StatisticType type, int maxValue)
        {
            Type = type;
            MaxValue = maxValue;
        }
    }
}
