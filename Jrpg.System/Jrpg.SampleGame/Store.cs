using System;
using System.Collections.Generic;
using System.IO;
using Jrpg.System;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.StatusEffects.Definitions;
namespace Jrpg.SampleGame
{
    public class Store
    {
        public GameStore Game { get; private set; }
        public Store()
        {
            Game = GameStore.GetInstance();
            Game.LoadConfig(File.ReadAllText("Configuration.json"));

            var techniqueDefinitions = new TechniqueFactory(Game.StatusEffectManager)
                .FromJsonDefinition(File.ReadAllText("Resources/Techniques.json"));

            Game.LoadTechniqueDefinitions(techniqueDefinitions);

            var ClassDefinitions = Game.JobClassManager.FromJsonDefinition(
                File.ReadAllText("Resources/CharacterClasses.json")
            );
            foreach (var className in ClassDefinitions.Keys)
            {
                Game.JobClassManager.Register(className, ClassDefinitions[className]);
            }

            var StatusEffectDefinitions = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<StatusEffectDefinition>>(File.ReadAllText("Resources/StatusEffects.json"));

            foreach (var definition in StatusEffectDefinitions)
            {
                Game.StatusEffectManager.RegisterStatusEffect(definition.DisplayName, definition);
            }
        }
    }
}
