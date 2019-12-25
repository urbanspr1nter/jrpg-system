using System;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem;
using System.Collections.Generic;

namespace Jrpg.SampleGame.Characters.Scalers.WhiteMage
{
    public class SpeedScaler : BaseScaler
    {
        public SpeedScaler()
        {
            type = StatisticType.Speed;
        }
    }
}
