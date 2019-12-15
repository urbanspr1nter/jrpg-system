using System;
using System.Collections.Generic;
using Jrpg.PartySystem;
using Jrpg.InventorySystem;
using Jrpg.GameState;
using Jrpg.CharacterSystem.StatusEffects;
using Jrpg.CharacterSystem.Classes;

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

        public Dictionary<string, object> DataStore { get; }

        private GameStore()
        {
            InventoryManager = new InventoryManager();
            MainParty = new Party(InventoryManager);
            GameStateManager = new GameStateManager(GameStateValue.Title);
            StatusEffectManager = new StatusEffectManager();
            JobClassManager = new ClassManager();
            DataStore = new Dictionary<string, object>();
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
            DataStore.Add(key, value);
        }

        public T Get<T>(string key)
        {
            if(!DataStore.ContainsKey(key))
            {
                throw new NullReferenceException("Key does not exist in Data Store.");
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
