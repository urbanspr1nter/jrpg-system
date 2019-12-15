using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.StatusEffects.Definitions;

namespace Jrpg.CharacterSystem.StatusEffects
{
    class StatusEffectFactory
    {
        private Dictionary<string, StatusEffectDefinition> registered;

        public StatusEffectFactory()
        {
            registered = new Dictionary<string, StatusEffectDefinition>();
        }

        public void Register(string name, StatusEffectDefinition definition)
        {
            registered[name] = definition;
        }

        public StatusEffect BuildStatusEffect(string name)
        {
            if(!registered.ContainsKey(name))
            {
                throw new KeyNotFoundException("The status effect definition was not found with this name.");
            }

            var definition = registered[name];
            var statusEffect = (StatusEffect)Activator
                .CreateInstance(Type.GetType(definition.Agent), new object[] { });

            return statusEffect;
        }
    }
}
