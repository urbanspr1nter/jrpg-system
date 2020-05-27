using Jrpg.MenuSystem;
using Jrpg.System;

namespace Jrpg.SampleGame.Menus.Options
{
    public class MenuOptionHandlerNoTest : MenuContentOptionHandler
    {
        public void Handle(GameStore g)
        {
            g.Put<string>("OptionResult", "NO");
        }
    }
}
