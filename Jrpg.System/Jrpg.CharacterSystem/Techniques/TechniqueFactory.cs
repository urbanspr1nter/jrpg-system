using System;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.CharacterSystem.Techniques.Concrete;

namespace Jrpg.CharacterSystem.Techniques
{
    public class TechniqueFactory
    {
        private StatusEffectManager StatusEffectManager;

        public TechniqueFactory(StatusEffectManager statusEffectManager)
        {
            StatusEffectManager = statusEffectManager;
        }

        public Technique GetTech(TechniqueName name)
        {
            if (name == TechniqueName.Regen)
            {
                return new Regen(StatusEffectManager);
            }
            else if (name == TechniqueName.Fire) {
                return new Fire(StatusEffectManager);
            }
            else if(name == TechniqueName.Fira)
            {
                return new Fira(StatusEffectManager);
            }
            else if (name == TechniqueName.Firaga)
            {
                return new Firaga(StatusEffectManager);
            }
            else
            {
                throw new NotSupportedException("No technique available to instantiate.");
            }
        }
    }
}
