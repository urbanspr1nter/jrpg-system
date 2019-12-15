/**
 * Aside from unit tests found in Jrpg.System.Tests, this project is also another easy way
 * to test the many features of the Jrpg.System package.
 *
 * Hence, you will see a lot of random weird code in this project. :)
 */ 
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Jrpg.System;
using Jrpg.CharacterSystem.StatusEffects.Definitions;
using Jrpg.SampleGame.Characters.JobClasses;
using Jrpg.CharacterSystem.Techniques;

namespace Jrpg.SampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameStore store = GameStore.GetInstance();

            store.LoadConfig(File.ReadAllText("Configuration.json"));

            var ClassDefinitions = store.JobClassManager.FromJsonDefinition(
                File.ReadAllText("Resources/CharacterClasses.json")
            );
            foreach(var className in ClassDefinitions.Keys)
            {
                store.JobClassManager.Register(className, ClassDefinitions[className]);
            }


            var StatusEffectDefinitions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StatusEffectDefinition>>(File.ReadAllText("Resources/StatusEffects.json"));
            foreach(var definition in StatusEffectDefinitions)
            {
                store.StatusEffectManager.RegisterStatusEffect(definition.DisplayName, definition);
            }

            var TechniqueDefinitions = new TechniqueFactory(store.StatusEffectManager).FromJsonDefinition(File.ReadAllText("Resources/Techniques.json"));

            var BlackMage = (BlackMage)store.JobClassManager.GetCharacterClassInstance("Black Mage");
            BlackMage.TechniqueDefinitions.Add(TechniqueDefinitions["Fire"]);
            BlackMage.TechniqueDefinitions.Add(TechniqueDefinitions["Fira"]);
            BlackMage.TechniqueDefinitions.Add(TechniqueDefinitions["Firaga"]);

            var WhiteMage = (WhiteMage)store.JobClassManager.GetCharacterClassInstance("White Mage");
            WhiteMage.TechniqueDefinitions.Add(TechniqueDefinitions["Regen"]);
        }
    }
}
