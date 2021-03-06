﻿using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.CharacterSystem.Classes.Definitions;
using Jrpg.CharacterSystem.Statistics;

namespace Jrpg.CharacterSystem.Classes
{
    public class ClassManager
    {
        private StatisticTypeRegistry statisticTypeRegistry;
        private Dictionary<string, ClassDefinition> registered;
        private Dictionary<string, TechniqueDefinition> techniqueDefinitions;

        public ClassManager(Dictionary<string, TechniqueDefinition> techDefs, StatisticTypeRegistry statTypeRegistry)
        {
            registered = new Dictionary<string, ClassDefinition>();
            techniqueDefinitions = techDefs;
            statisticTypeRegistry = statTypeRegistry;
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
            var jobClassStartingStatistics = new StatisticRegistry();

            foreach(var classStatistic in classDefinition.StartingStatistics)
            {
                var statisticType = statisticTypeRegistry.FindByName(classStatistic.Type);
                var statistic = new Statistic(statisticType, classStatistic.DefaultValue, classStatistic.MaxValue, new BaseStatisticModifier());

                jobClassStartingStatistics.Add(statisticType, statistic);
            }

            List<ClassTechniqueDefinition> techniqueDefinitionMappings = classDefinition.Techniques;
            List<TechniqueDefinition> techniqueDefinitionsForClass = new List<TechniqueDefinition>();
                
            foreach(var classTechniqueDefinition in techniqueDefinitionMappings)
            {
                techniqueDefinitionsForClass.Add(techniqueDefinitions[classTechniqueDefinition.Name]);
            }

            var jobClassInstance = (BaseCharacterClass)Activator
                .CreateInstance(
                Type.GetType(jobClassAgent),
                new object[] {
                    jobClassStartingStatistics,
                    techniqueDefinitionsForClass,
                    techniqueDefinitionMappings
                }
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
