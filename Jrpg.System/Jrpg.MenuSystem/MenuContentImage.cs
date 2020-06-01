using Jrpg.System;
using System;

namespace Jrpg.MenuSystem
{
    public class MenuContentImage : MenuContent
    {
        public string Content { get; set; }

        public MenuContentImage(GameStore g) : base(g) { }
    }
}
