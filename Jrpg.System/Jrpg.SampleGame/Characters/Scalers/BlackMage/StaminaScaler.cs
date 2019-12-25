﻿using System;
using Jrpg.CharacterSystem.Scalers;
using Jrpg.CharacterSystem;
using System.Collections.Generic;

namespace Jrpg.SampleGame.Characters.Scalers.BlackMage
{
    public class StaminaScaler : BaseScaler
    {
        public StaminaScaler()
        {
            type = StatisticType.Stamina;
        }
    }
}
