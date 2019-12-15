using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.StatusEffects;

namespace Jrpg.CharacterSystem.Techniques
{
    public abstract class Technique : IPerformable
    {
        protected StatusEffectManager StatusEffectManager;
        public TechniqueDefinition Definition { get; protected set; }

        public abstract void Perform(Character source, List<Character> targets);

        public Technique(StatusEffectManager statusEffectManager, TechniqueDefinition definition) {
            StatusEffectManager = statusEffectManager;
            Definition = definition;
        }
    }
}
