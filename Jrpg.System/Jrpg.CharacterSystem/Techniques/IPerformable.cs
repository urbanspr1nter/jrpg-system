using System;
using System.Collections.Generic;

namespace Jrpg.CharacterSystem.Techniques
{
    public interface IPerformable
    {
        void Perform(Character source, List<Character> targets);
    }
}
