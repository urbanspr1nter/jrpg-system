using System;
using System.IO;
using System.Collections.Generic;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.CharacterSystem.Classes;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.StatusEffects.Definitions;
using Jrpg.BattleSystem.Enemies;
using Jrpg.GameState;
using Jrpg.SampleGame.Characters.JobClasses;
using Jrpg.InventorySystem.PgItems;

namespace Jrpg.System.Tests
{
    public class MockGameLoop
    {
        private GameStateManager gameStateManager;
        public ClassManager ClassManager { get; private set; }
        public StatusEffectManager StatusEffectManager { get; private set; }
        public EnemyManager EnemyManager { get; private set; }

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

            InitializeEnemeyManager(TechniqueDefinitions);

            var StatusEffectDefinitions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StatusEffectDefinition>>(File.ReadAllText("Resources/StatusEffects.json"));
            foreach (var definition in StatusEffectDefinitions)
            {
                StatusEffectManager.RegisterStatusEffect(definition.DisplayName, definition);
            }


            BlackMage = (BlackMage)ClassManager.GetCharacterClassInstance("Black Mage");
            WhiteMage = (WhiteMage)ClassManager.GetCharacterClassInstance("White Mage");

            gameStateManager.Register(StatusEffectManager);
        }

        private void InitializeEnemeyManager(Dictionary<string, TechniqueDefinition> techDefs)
        {
            var tonicItem = new Item()
            {
                Name = "Tonic",
                ItemClass = new List<ItemClassEdge>(),
                Properties = new List<Property>() {
                        new Property() {
                            Name= "HP Current",
                            Value = 10
                        }
                    },
                Value = 200,
                BodyPart = "Default"
            };

            var antidoteItem = new Item()
            {
                Name = "Antidote",
                ItemClass = new List<ItemClassEdge>(),
                Properties = new List<Property>() {
                        new Property() {
                            Name= "HP Current",
                            Value = 10
                        }
                    },
                Value = 200,
                BodyPart = "Default"
            };

            var mockItemDefs = new List<Item>() { tonicItem, antidoteItem };


            var noPrefix = new Affix()
            {
                Name = "NoPrefix",
                ParentItemClass = new List<string>(),
                Properties = new List<Property>(),
                Weight = 15,
                Value = new ValueObject()
            };


            var mockPrefixDefs = new List<Affix>() { noPrefix };

            var noSuffix = new Affix()
            {
                Name = "NoSuffix",
                ParentItemClass = new List<string>(),
                Properties = new List<Property>(),
                Weight = 15,
                Value = new ValueObject()
            };


            var mockSuffixDefs = new List<Affix>() { noSuffix };

            EnemyManager
                = new EnemyManager(
                    techDefs,
                    new ItemGenerator(mockItemDefs, mockPrefixDefs, mockSuffixDefs)
                );
            EnemyManager.FromJsonDefinition(File.ReadAllText("Resources/Enemies.json"));
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
