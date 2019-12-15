using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.StatusEffects;

namespace Jrpg.CharacterSystem.Techniques
{
    public class TechniqueFactory
    {
        private StatusEffectManager StatusEffectManager;

        public TechniqueFactory(StatusEffectManager statusEffectManager)
        {
            StatusEffectManager = statusEffectManager;
        }

        public Technique CreateTechnique(TechniqueDefinition definition)
        {
            try
            {
                var instance = Activator.CreateInstance(
                    Type.GetType(definition.Agent),
                    new object[] { StatusEffectManager, definition }
                );

                return (Technique)(instance);
            } catch(TypeLoadException e)
            {
                Console.WriteLine("Couldn't load the technique");
                throw e;
            }
        }

        public Dictionary<string, TechniqueDefinition> FromJsonDefinition(string json)
        {
            var definitions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TechniqueDefinition>>(json);

            var results = new Dictionary<string, TechniqueDefinition>();

            foreach(var definition in definitions)
            {
                results.Add(definition.DisplayName, definition);
            }

            return results;
        }
    }
}
