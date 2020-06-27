using System;
using System.Collections.Generic;
using Jrpg.System;

namespace Jrpg.MenuSystem.Components.Replacers
{
    public abstract class BaseLogReplacer5 : MenuContentTokenReplacer
    {
        protected int LineNumber;

        public BaseLogReplacer5(string token) : base(token)
        {
        }

        public override string Replace(GameStore g)
        {
            try
            {
                List<string> line = g.Get<List<string>>("DisplayLines");

                return line[LineNumber - 1];
            }
            catch
            {
                return string.Empty;
            }

        }
    }

    public class LogReplacer5Line1 : BaseLogReplacer5
    {
        public LogReplacer5Line1(string token) : base(token)
        {
            LineNumber = 1;
        }
    }

    public class LogReplacer5Line2 : BaseLogReplacer5
    {
        public LogReplacer5Line2(string token) : base(token)
        {
            LineNumber = 2;
        }
    }

    public class LogReplacer5Line3 : BaseLogReplacer5
    {
        public LogReplacer5Line3(string token) : base(token)
        {
            LineNumber = 3;
        }

    }

    public class LogReplacer5Line4 : BaseLogReplacer5
    {
        public LogReplacer5Line4(string token) : base(token)
        {
            LineNumber = 4;
        }
    }

    public class LogReplacer5Line5 : BaseLogReplacer5
    {
        public LogReplacer5Line5(string token) : base(token)
        {
            LineNumber = 5;
        }
    }
}
