using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Classes;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.Classes.Definitions;
using Jrpg.SampleGame.Characters.Scalers.BlackMage;

namespace Jrpg.SampleGame.Characters.JobClasses
{
    public class BlackMage : BaseCharacterClass
    {
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

        public BlackMage(Dictionary<StatisticType, Statistic> statistics,
            List<TechniqueDefinition> techniqueDefinitions,
            List<ClassTechniqueDefinition> techniqueDefinitionMapping)
            : base(statistics, techniqueDefinitions, techniqueDefinitionMapping)
        {
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

        public override Statistic NextHpMax()
        {
            return hpScaler.NewStatistic(Statistics);
        }

        public override Statistic NextMpMax()
        {
            return mpScaler.NewStatistic(Statistics);
        }

        public override Statistic NextStrength()
        {
            return strengthScaler.NewStatistic(Statistics);
        }

        public override Statistic NextSpeed()
        {
            return speedScaler.NewStatistic(Statistics);
        }

        public override Statistic NextStamina()
        {
            return staminaScaler.NewStatistic(Statistics);
        }

        public override Statistic NextMagic()
        {
            return magicScaler.NewStatistic(Statistics);
        }

        public override Statistic NextAttack()
        {
            return attackScaler.NewStatistic(Statistics);
        }

        public override Statistic NextDefense()
        {
            return defenseScaler.NewStatistic(Statistics);
        }

        public override Statistic NextEvasion()
        {
            return evasionScaler.NewStatistic(Statistics);
        }

        public override Statistic NextMagicDefense()
        {
            return magicDefenseScaler.NewStatistic(Statistics);
        }

        public override Statistic NextMagicEvasion()
        {
            return magicEvasionScaler.NewStatistic(Statistics);
        }

        public override string ClassName()
        {
            return "Black Mage";
        }
    }
}
