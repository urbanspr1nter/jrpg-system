using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.StatusEffects;

namespace Jrpg.CharacterSystem.Techniques
{
    public abstract class Technique : IPerformable
    {
        protected StatusEffectManager StatusEffectManager;

        public string Id { get; protected set; }
        public string DisplayName { get; protected set; }
        public int MpCost { get; protected set; }
        public int AttackPower { get; protected set; }
        public int MagicPower { get; protected set; }

        public abstract void Perform(Character source, List<Character> targets);

        public Technique(StatusEffectManager statusEffectManager) {
            StatusEffectManager = statusEffectManager;
        }
    }
}
