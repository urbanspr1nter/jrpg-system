﻿using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.Classes.Definitions;

namespace Jrpg.CharacterSystem.Classes
{
    public class ClassManager
    {
        private Dictionary<string, ClassDefinition> registered;

        public ClassManager()
        {
            registered = new Dictionary<string, ClassDefinition>();
        }

        public void Register(string tag, ClassDefinition classDefinition)
        {
            if(registered.ContainsKey(tag))
            {
                registered[tag] = classDefinition;

                return;
            }

            registered.Add(tag, classDefinition);
        }

        public string GetCharacterClassAgent(string tag)
        {
            if (!registered.ContainsKey(tag))
            {
                throw new KeyNotFoundException("No registered job class found with this tag.");
            }

            return registered[tag].Agent;
        }

        public BaseCharacterClass GetCharacterClassInstance(string characterClassTag)
        {
            if(!registered.ContainsKey(characterClassTag))
            {
                throw new KeyNotFoundException($"Couldn't find the character class type with key {characterClassTag}.");
            }

            var classDefinition = registered[characterClassTag];
            var jobClassAgent = classDefinition.Agent;
            var jobClassStartingStatistics = new Dictionary<StatisticType, Statistic>();

            foreach(var classStatistic in classDefinition.StartingStatistics)
            {
                var statisticType = CommonUtils.ToStatisticType(classStatistic.Name);
                var statistic = CommonUtils.ToStatistic(classStatistic);

                jobClassStartingStatistics.Add(statisticType, statistic);
            }

            var jobClassInstance = (BaseCharacterClass)Activator
                .CreateInstance(
                Type.GetType(jobClassAgent),
                new object[] { jobClassStartingStatistics }
            );

            return jobClassInstance;
        }

        public Dictionary<string, ClassDefinition> FromJsonDefinition(string json)
        {
            var definitions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassDefinition>>(json);

            var results = new Dictionary<string, ClassDefinition>();

            foreach(var definition in definitions)
            {
                results.Add(definition.Name, definition);
            }

            return results;
        }
    }
}
