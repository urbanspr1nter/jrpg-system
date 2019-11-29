using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem.Scalers.Freelancer;

namespace Jrpg.CharacterSystem.Classes
{
    public class Freelancer
    {
        private Dictionary<StatisticType, Statistic> Statistics;

        private IStatisticScaler levelScaler;
        private IStatisticScaler hpScaler;
        private IStatisticScaler mpScaler;
        private IStatisticScaler strengthScaler;
        private IStatisticScaler speedScaler;
        private IStatisticScaler staminaScaler;
        private IStatisticScaler magicScaler;
        private IStatisticScaler attackScaler;
        private IStatisticScaler defenseScaler;
        private IStatisticScaler evasionScaler;
        private IStatisticScaler magicDefenseScaler;
        private IStatisticScaler magicEvasionScaler;

        public Freelancer(Dictionary<StatisticType, Statistic> statistics)
        {
            Statistics = statistics;

            levelScaler = new LevelScaler();
            hpScaler = new HpScaler();
            mpScaler = new MpScaler();
            strengthScaler = new StrengthScaler();
            speedScaler = new SpeedScaler();
            staminaScaler = new StaminaScaler();
            magicScaler = new MagicScaler();
            attackScaler = new AttackScaler();
            defenseScaler = new DefenseScaler();
            evasionScaler = new EvasionScaler();
            magicDefenseScaler = new MagicDefenseScaler();
            magicEvasionScaler = new MagicEvasionScaler();
        }

        private Statistic NextLevel()
        {
            return levelScaler.NewStatistic(Statistics);
        }

        private Statistic NextHpMax()
        {
            return hpScaler.NewStatistic(Statistics);
        }

        private Statistic NextMpMax()
        {
            return mpScaler.NewStatistic(Statistics);
        }

        private Statistic NextStrength()
        {
            return strengthScaler.NewStatistic(Statistics);
        }

        private Statistic NextSpeed()
        {
            return speedScaler.NewStatistic(Statistics);
        }

        private Statistic NextStamina()
        {
            return staminaScaler.NewStatistic(Statistics);
        }

        private Statistic NextMagic()
        {
            return magicScaler.NewStatistic(Statistics);
        }

        private Statistic NextAttack()
        {
            return attackScaler.NewStatistic(Statistics);
        }

        private Statistic NextDefense()
        {
            return defenseScaler.NewStatistic(Statistics);
        }

        private Statistic NextEvasion()
        {
            return evasionScaler.NewStatistic(Statistics);
        }

        private Statistic NextMagicDefense()
        {
            return magicDefenseScaler.NewStatistic(Statistics);
        }

        private Statistic NextMagicEvasion()
        {
            return magicEvasionScaler.NewStatistic(Statistics);
        }

        public string ClassName()
        {
            return "Freelancer";
        }

        public void LevelUp()
        {
            Statistics[StatisticType.Level] = NextLevel();
            Statistics[StatisticType.HpMax] = NextHpMax();
            Statistics[StatisticType.MpMax] = NextMpMax();
            Statistics[StatisticType.Strength] = NextStrength();
            Statistics[StatisticType.Speed] = NextSpeed();
            Statistics[StatisticType.Stamina] = NextStamina();
            Statistics[StatisticType.Magic] = NextMagic();
            Statistics[StatisticType.Attack] = NextAttack();
            Statistics[StatisticType.Defense] = NextDefense();
            Statistics[StatisticType.Evasion] = NextEvasion();
            Statistics[StatisticType.MagicDefense] = NextMagicDefense();
            Statistics[StatisticType.MagicEvasion] = NextMagicEvasion();
        }
    }
}
