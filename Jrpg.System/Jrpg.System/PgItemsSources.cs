using System;
using System.Collections.Generic;
namespace Jrpg.System
{
    public class PgItemsSources
    {
        public string ItemsPath { get; set; }
        public string PrefixesPath { get; set; }
        public string SuffixesPath { get; set; }
        public Dictionary<string, string> DropSourcesPath { get; set; }
    }
}
