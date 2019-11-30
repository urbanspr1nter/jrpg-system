using System;
using Jrpg.GameState;
using System.Collections.Generic;

namespace Jrpg.CharacterSystem.StatusEffects
{
    public class StatusEffectManager : IGameStateSubscriber
    {
        private GameStateValue _state;
        private Dictionary<Character, List<StatusEffect>> map;

        public StatusEffectManager()
        {
            map = new Dictionary<Character, List<StatusEffect>>();
        }

        public void ReceiveStateUpdate(GameStateValue state)
        {
            _state = state;
        }

        public void ApplyEffect(Character character, StatusEffectType statusEffectType)
        {
            if(!map.ContainsKey(character))
            {
                map.Add(character, new List<StatusEffect>());
            }

            // Cannot stack status effects
            if(map[character].Find(effect => effect.GetStatusEffectType() == statusEffectType) != null)
            {
                return;
            }

            var statusEffect = StatusEffectFactory.BuildStatusEffect(statusEffectType);
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

        public void RemoveEffect(Character character, StatusEffectType statusEffectType)
        {
            if(!map.ContainsKey(character))
            {
                return;
            }

            StatusEffect toRemove = null;
            foreach(var statusEffect in map[character])
            {
                if(statusEffect.GetStatusEffectType() == statusEffectType)
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

        public List<StatusEffectType> StatusEffectTypes(Character character)
        {
            if(!map.ContainsKey(character))
            {
                return new List<StatusEffectType>();
            }

            List<StatusEffectType> result = new List<StatusEffectType>();

            foreach(var statusEffect in map[character])
            {
                result.Add(statusEffect.GetStatusEffectType());
            }

            return result;
        }
    }
}
