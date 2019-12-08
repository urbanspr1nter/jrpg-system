using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem.Scalers.Freelancer;
using Jrpg.CharacterSystem.Techniques;

namespace Jrpg.CharacterSystem.Classes
{
    class Freelancer : ICharacterClass
    {
        private Dictionary<StatisticType, Statistic> Statistics;
        public List<TechniqueName> TechniqueNames { get; private set; }

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

            TechniqueNames = new List<TechniqueName>();
        }

        public Statistic NextLevel()
        {
            return levelScaler.NewStatistic(Statistics);
        }

        public Statistic NextHpMax()
        {
            return hpScaler.NewStatistic(Statistics);
        }

        public Statistic NextMpMax()
        {
            return mpScaler.NewStatistic(Statistics);
        }

        public Statistic NextStrength()
        {
            return strengthScaler.NewStatistic(Statistics);
        }

        public Statistic NextSpeed()
        {
            return speedScaler.NewStatistic(Statistics);
        }

        public Statistic NextStamina()
        {
            return staminaScaler.NewStatistic(Statistics);
        }

        public Statistic NextMagic()
        {
            return magicScaler.NewStatistic(Statistics);
        }

        public Statistic NextAttack()
        {
            return attackScaler.NewStatistic(Statistics);
        }

        public Statistic NextDefense()
        {
            return defenseScaler.NewStatistic(Statistics);
        }

        public Statistic NextEvasion()
        {
            return evasionScaler.NewStatistic(Statistics);
        }

        public Statistic NextMagicDefense()
        {
            return magicDefenseScaler.NewStatistic(Statistics);
        }

        public Statistic NextMagicEvasion()
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

            if(Statistics[StatisticType.Level].CurrentValue == 2)
            {
                TechniqueNames.Add(TechniqueName.Regen);
                TechniqueNames.Add(TechniqueName.Fire);
                TechniqueNames.Add(TechniqueName.Fira);
                TechniqueNames.Add(TechniqueName.Firaga);
            }
        }
    }
}
