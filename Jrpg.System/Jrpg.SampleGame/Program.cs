using System;
using System.Collections.Generic;
using System.IO;
using Jrpg.System;

namespace Jrpg.SampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameStore store = GameStore.GetInstance();

            store.LoadConfig(File.ReadAllText("Configuration.json"));

            List<string> names = store.Config.Get<List<string>>("MemberNames");
            var myTest = store.Config.Get<ComplexObject>("someObject");
            Console.WriteLine(names[0] + " " + names[1]);
        }
    }
}
