/**
 * Aside from unit tests found in Jrpg.System.Tests, this project is also another easy way
 * to test the many features of the Jrpg.System package.
 *
 * Hence, you will see a lot of random weird code in this project. :)
 */ 
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Jrpg.System;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.StatusEffects.Definitions;
using Jrpg.SampleGame.Characters.JobClasses;
using Jrpg.CharacterSystem.Techniques;
using Jrpg.BattleSystem.Enemies;
using Jrpg.InventorySystem.PgItems;

namespace Jrpg.SampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();


            Enemy Imp = store.Game.EnemyManager.GetEnemyInstance("Imp", "Basic Goblin");

            for(int i = 100; i < 125; i ++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"{i}. {Imp.Name}, {Imp.GetItem().Name}");
            }

        }
    }
}
