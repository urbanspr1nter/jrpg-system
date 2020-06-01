using System;
using Jrpg.System;

namespace Jrpg.MenuSystem
{
    public class MenuContentText : MenuContent
    {
        public string Content { get; set; }

        public MenuContentText(GameStore g) : base(g)
        {
            Type = MenuContentType.Text;
        }
    }
}
