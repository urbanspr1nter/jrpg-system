using System;
using System.Collections.Generic;

namespace Jrpg.CharacterSystem.Techniques
{
    public class TechniqueName
    {
        public string Name { get; set; }

        public TechniqueName(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            if(ReferenceEquals(this, obj))
            {
                return true;
            }

            if(GetType() != obj.GetType())
            {
                return false;
            }

            var that = (TechniqueName)obj;

            return Name.Equals(that.Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
