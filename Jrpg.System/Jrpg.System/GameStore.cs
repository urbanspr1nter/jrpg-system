using System;
using System.Collections.Generic;
using Jrpg.PartySystem;
using Jrpg.InventorySystem;
using Jrpg.GameState;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.CharacterSystem.Classes;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.BattleSystem.Enemies;
using Jrpg.InventorySystem.PgItems;

namespace Jrpg.System
{
    public class GameStore
    {
        private static GameStore Instance;

        private InventoryManager InventoryManager;
        private GameStateManager GameStateManager;

        public Configuration Config { get; private set; }
        public Party MainParty { get; private set; }
        public StatusEffectManager StatusEffectManager { get; private set; }
        public ClassManager JobClassManager { get; private set; }
        public EnemyManager EnemyManager { get; private set; }

        public Dictionary<string, object> DataStore { get; }

        private GameStore()
        {
            InventoryManager = new InventoryManager();
            MainParty = new Party(InventoryManager);
            GameStateManager = new GameStateManager(GameStateValue.Title);
            StatusEffectManager = new StatusEffectManager();
            JobClassManager = new ClassManager(new Dictionary<string, TechniqueDefinition>());
            DataStore = new Dictionary<string, object>();
        }

        public void InitializeManagers(Dictionary<string, TechniqueDefinition> techDefs, List<Item> items, List<Affix> prefixes, List<Affix> suffixes)
        {
            JobClassManager = new ClassManager(techDefs);
            EnemyManager = new EnemyManager(techDefs, new ItemGenerator(items, prefixes, suffixes));
        }

        public void SubscribeToGameState(IGameStateSubscriber subscriber)
        {
            GameStateManager.Register(subscriber);
        }

        public void UnsubscribeToGameState(IGameStateSubscriber subscriber)
        {
            GameStateManager.Unregister(subscriber);
        }

        public void SetGameState(GameStateValue state)
        {
            GameStateManager.PublishStateUpdate(state);
        }

        public GameStateValue CurrentGameState()
        {
            return GameStateManager.CurrentState();
        }

        public void Put<T>(string key, T value)
        {
            if (!DataStore.ContainsKey(key))
            {
                DataStore.Add(key, null);
            }

            if (DataStore.ContainsKey(key))
                DataStore[key] = value;
            else
                DataStore.Add(key, value);
        }

        public T Get<T>(string key)
        {
            if(!DataStore.ContainsKey(key))
            {
                return default(T);
            }

            return (T)DataStore[key];
        }

        public void LoadConfig(string contents)
        {
            Config = new Configuration(contents);
        }

        public static GameStore GetInstance()
        {
            if(Instance == null)
            {
                Instance = new GameStore();
            }

            return Instance;
        }
    }
}
