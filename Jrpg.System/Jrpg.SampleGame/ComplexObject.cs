using System;
using System.Collections.Generic;
using System.Drawing;
namespace Jrpg.SampleGame
{
    public class ComplexObject
    {
        public int Value { get; set; }
        public string StringValue { get; set; }
        public bool FlagValue { get; set; }
        public List<string> ListValue { get; set; }
        public Location LocationValue { get; set; }
    }

    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
