using System;
namespace Jrpg.CharacterSystem.Classes
{
    interface ICharacterClass
    {
        Statistic NextLevel();
        Statistic NextHpMax();
        Statistic NextMpMax();
        Statistic NextStrength();
        Statistic NextSpeed();
        Statistic NextStamina();
        Statistic NextMagic();
        Statistic NextAttack();
        Statistic NextDefense();
        Statistic NextEvasion();
        Statistic NextMagicDefense();
        Statistic NextMagicEvasion();
        string ClassName();
        void LevelUp();
    }
}
