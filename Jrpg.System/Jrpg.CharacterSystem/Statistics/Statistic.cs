namespace Jrpg.CharacterSystem.Statistics
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

        public int DefaultValue { get; }

        public int MaxValue { get; }

        private IStatisticModifier Modifier;
        
        public Statistic(StatisticType type, int defaultValue, int maxValue, IStatisticModifier modifier)
        {
            Type = type;
            MaxValue = maxValue;
            Modifier = modifier;
            DefaultValue = defaultValue;
            _currentValue = defaultValue;
        }

        public void Up()
        {
            Modifier.HandleUp();
        }

        public void Down()
        {
            Modifier.HandleDown();
        }

        public Statistic Clone()
        {
            var copy = new Statistic(Type, DefaultValue, MaxValue, Modifier);

            copy.CurrentValue = CurrentValue;

            return copy;
        }
    }
}
