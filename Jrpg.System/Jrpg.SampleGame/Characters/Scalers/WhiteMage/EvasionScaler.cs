using System;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem;
using System.Collections.Generic;

namespace Jrpg.SampleGame.Characters.Scalers.WhiteMage
{
    public class EvasionScaler : BaseScaler
    {
        public EvasionScaler()
        {
            type = StatisticType.Evasion;
        }
    }
}
