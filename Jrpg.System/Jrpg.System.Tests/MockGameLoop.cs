using System;
using System.IO;
using System.Collections.Generic;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.CharacterSystem.Classes;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.StatusEffects.Definitions;
using Jrpg.GameState;
using Jrpg.SampleGame.Characters.JobClasses;

namespace Jrpg.System.Tests
{
    public class MockGameLoop
    {
        private GameStateManager gameStateManager;
        public ClassManager ClassManager { get; private set; }
        public StatusEffectManager StatusEffectManager { get; private set; }

        public BlackMage BlackMage { get; private set; }
        public WhiteMage WhiteMage { get; private set; }

        public MockGameLoop()
        {
            gameStateManager = new GameStateManager(GameStateValue.World);
            StatusEffectManager = new StatusEffectManager();
            var TechniqueDefinitions = new TechniqueFactory(StatusEffectManager).FromJsonDefinition(File.ReadAllText("Resources/Techniques.json"));

            ClassManager = new ClassManager(TechniqueDefinitions);
            var ClassDefinitions = ClassManager.FromJsonDefinition(
                File.ReadAllText("Resources/CharacterClasses.json")
            );
            foreach (var className in ClassDefinitions.Keys)
            {
                ClassManager.Register(className, ClassDefinitions[className]);
            }

            var StatusEffectDefinitions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StatusEffectDefinition>>(File.ReadAllText("Resources/StatusEffects.json"));
            foreach (var definition in StatusEffectDefinitions)
            {
                StatusEffectManager.RegisterStatusEffect(definition.DisplayName, definition);
            }


            BlackMage = (BlackMage)ClassManager.GetCharacterClassInstance("Black Mage");
            WhiteMage = (WhiteMage)ClassManager.GetCharacterClassInstance("White Mage");

            gameStateManager.Register(StatusEffectManager);
        }

        public void SetGameState(GameStateValue state)
        {
            gameStateManager.PublishStateUpdate(state);
        }

        public GameStateValue GetGameState()
        {
            return gameStateManager.CurrentState();
        }

        public void Step()
        {
            StatusEffectManager.BeforeEffects();
            StatusEffectManager.PerformEffects();
            StatusEffectManager.AfterEffects();
            StatusEffectManager.CleanUp();
        }
    }
}
