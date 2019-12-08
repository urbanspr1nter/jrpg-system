/**
 * Represents a basic Enemy class. All monsters are Character types with this
 * specific job class as the default job class.
 */ 
using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.Techniques;

namespace Jrpg.CharacterSystem.Classes
{
    public class Enemy : ICharacterClass
    {
        private Dictionary<StatisticType, Statistic> Statistics;
        public List<TechniqueName> TechniqueNames { get; private set; }

        public Enemy(Dictionary<StatisticType, Statistic> statistics)
        {
            Statistics = statistics;
        }

        public string ClassName()
        {
            return "Enemy";
        }

        public void LevelUp()
        {
            return;
        }

        public Statistic NextAttack()
        {
            return Statistics[StatisticType.Attack];
        }

        public Statistic NextDefense()
        {
            return Statistics[StatisticType.Defense];
        }

        public Statistic NextEvasion()
        {
            return Statistics[StatisticType.Evasion];
        }

        public Statistic NextHpMax()
        {
            return Statistics[StatisticType.HpMax];
        }

        public Statistic NextLevel()
        {
            return Statistics[StatisticType.Level];
        }

        public Statistic NextMagic()
        {
            return Statistics[StatisticType.Magic];
        }

        public Statistic NextMagicDefense()
        {
            return Statistics[StatisticType.MagicDefense];
        }

        public Statistic NextMagicEvasion()
        {
            return Statistics[StatisticType.MagicEvasion];
        }

        public Statistic NextMpMax()
        {
            return Statistics[StatisticType.MpMax];
        }

        public Statistic NextSpeed()
        {
            return Statistics[StatisticType.Speed];
        }

        public Statistic NextStamina()
        {
            return Statistics[StatisticType.Stamina];
        }

        public Statistic NextStrength()
        {
            return Statistics[StatisticType.Strength];
        }
    }
}
