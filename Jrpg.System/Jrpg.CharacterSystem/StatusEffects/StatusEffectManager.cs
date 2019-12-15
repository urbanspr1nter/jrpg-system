using System;
using Jrpg.GameState;
using System.Collections.Generic;
using Jrpg.CharacterSystem.StatusEffects.Definitions;

namespace Jrpg.CharacterSystem.StatusEffects
{
    public class StatusEffectManager : IGameStateSubscriber
    {
        private GameStateValue _state;
        private Dictionary<Character, List<StatusEffect>> map;
        private StatusEffectFactory factory;

        public StatusEffectManager()
        {
            map = new Dictionary<Character, List<StatusEffect>>();
            factory = new StatusEffectFactory();
        }

        public void RegisterStatusEffect(string name, StatusEffectDefinition definition)
        {
            factory.Register(name, definition);
        }

        public void ReceiveStateUpdate(GameStateValue state)
        {
            _state = state;
        }

        public void ApplyEffect(Character character, string name)
        {
            if(!map.ContainsKey(character))
            {
                map.Add(character, new List<StatusEffect>());
            }

            // Cannot stack status effects
            if(map[character].Find(effect => effect.GetStatusEffectName().Equals(name)) != null)
            {
                return;
            }

            var statusEffect = factory.BuildStatusEffect(name);
            map[character].Add(statusEffect);
            statusEffect.OnApply(character, _state);
        }

        public void BeforeEffects()
        {
            foreach (var character in map.Keys)
            {
                foreach (var statusEffect in map[character])
                {
                    statusEffect.BeforeEffect(character, _state);
                }
            }
        }

        public void PerformEffects()
        {
            foreach(var character in map.Keys)
            {
                foreach(var statusEffect in map[character])
                {
                    statusEffect.PerformEffect(character, _state);
                }
            }
        }

        public void AfterEffects()
        {
            foreach (var character in map.Keys)
            {
                foreach (var statusEffect in map[character])
                {
                    statusEffect.AfterEffect(character, _state);
                }
            }
        }

        public void RemoveEffect(Character character, string statusEffectName)
        {
            if(!map.ContainsKey(character))
            {
                return;
            }

            StatusEffect toRemove = null;
            foreach(var statusEffect in map[character])
            {
                if(statusEffect.GetStatusEffectName().Equals(statusEffectName))
                {
                    toRemove = statusEffect;
                    break;
                }
            }

            if(toRemove == null)
            {
                return;
            }

            toRemove.OnRemove(character, _state);

            map[character].Remove(toRemove);
        }

        public void CleanUp()
        {
            foreach(var character in map.Keys)
            {
                map[character].RemoveAll(effect => effect.ShouldDestroy(character, _state));
            }
        }

        public List<string> StatusEffectNames(Character character)
        {
            if(!map.ContainsKey(character))
            {
                return new List<string>();
            }

            List<string> result = new List<string>();

            foreach(var statusEffect in map[character])
            {
                result.Add(statusEffect.GetStatusEffectName());
            }

            return result;
        }
    }
}
