using Jrpg.MenuSystem;
using Jrpg.System;

namespace Jrpg.SampleGame.Menus.Options
{
    public class MenuOptionHanderYesTest : MenuContentOptionHandler
    {
        public void Handle(GameStore g)
        {
            g.Put<string>("OptionResult", "YES");
        }
    }
}
