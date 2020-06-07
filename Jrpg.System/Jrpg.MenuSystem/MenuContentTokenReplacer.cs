using System;
using System.Collections.Generic;
using System.Text;
using Jrpg.System;

namespace Jrpg.MenuSystem
{
    public abstract class MenuContentTokenReplacer
    {
        public string Token { get; set; }
        public abstract string Replace(GameStore g);

        public MenuContentTokenReplacer(string token)
        {
            Token = token;
        }
    }
}
