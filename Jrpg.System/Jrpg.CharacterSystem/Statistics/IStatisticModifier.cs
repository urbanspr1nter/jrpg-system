using System;
using System.Collections.Generic;
using System.Text;

namespace Jrpg.CharacterSystem.Statistics
{
    public interface IStatisticModifier
    {
        void HandleUp();
        void HandleDown();
    }
}
