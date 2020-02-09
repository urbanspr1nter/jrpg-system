using System;
using System.IO;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.BattleSystem.Enemies.Definitions;
using Jrpg.InventorySystem.PgItems;

namespace Jrpg.BattleSystem.Enemies
{
    public class EnemyManager
    {
        private Dictionary<string, EnemyDefinition> enemies;
        private Dictionary<string, TechniqueDefinition> techniqueDefinitions;
        private ItemGenerator itemGenerator;

        public EnemyManager(
            Dictionary<string, TechniqueDefinition> techDefs,
            ItemGenerator itemGenerator
        )
        {
            enemies = new Dictionary<string, EnemyDefinition>();
            techniqueDefinitions = techDefs;
            this.itemGenerator = itemGenerator;
        }

        public void Register(string tag, EnemyDefinition enemyDefinition)
        {
            if(enemies.ContainsKey(tag))
            {
                return;
            }

            enemies.Add(tag, enemyDefinition);
        }

        public Enemy GetEnemyInstance(string name, string className)
        {
            var enemyClass = GetEnemyClass(className);

            return new Enemy(name, enemyClass, itemGenerator);
        }

        public EnemyClass GetEnemyClass(string name)
        {
            if(!enemies.ContainsKey(name))
            {
                throw new KeyNotFoundException("No registered enemy found.");
            }

            var enemyDefinition = enemies[name];
            var agent = enemyDefinition.Agent;
            var startingStatistics = new Dictionary<StatisticType, Statistic>();

            foreach(var enemyStatistic in enemyDefinition.StartingStatistics)
            {
                var type = CommonUtils.ToStatisticType(enemyStatistic.Name);
                var statistic = CommonUtils.ToStatistic(enemyStatistic);

                startingStatistics.Add(type, statistic);
            }

            var techniqueDefinitionMapping = enemyDefinition.Techniques;
            var techniqueDefinitionsForEnemy = new List<TechniqueDefinition>();

            foreach(var classTechniqueDefinition in techniqueDefinitionMapping)
            {
                techniqueDefinitionsForEnemy.
                    Add(techniqueDefinitions[classTechniqueDefinition.Name]);
            }

            var enemyClass = (EnemyClass)(Activator.CreateInstance(Type.GetType(agent),
                new object[]
            {
                startingStatistics,
                techniqueDefinitionsForEnemy,
                techniqueDefinitionMapping,
                enemyDefinition.ItemClass,
                enemyDefinition.Gold,
                enemyDefinition.Experience
            }));

            return enemyClass;
        }

        public void FromJsonDefinition(string json)
        {
            List<EnemyDefinition> loaded
                = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EnemyDefinition>>
                    (json);

            foreach(var enemy in loaded)
            {
                Register(enemy.Name, enemy);
            }
        }
    }
}
