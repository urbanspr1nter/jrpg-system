using Jrpg.MenuSystem;
using Jrpg.System;

namespace Jrpg.SampleGame.Menus.Tokens
{
    public class MenuTokenReplacerNameTest : MenuContentTokenReplacer
    {
        public MenuTokenReplacerNameTest() : base()
        {
            Token = "$NAME$";
        }
        public override string Replace(GameStore g)
        {
            return g.Get<string>("MainCharacterName");
        }
    }
}
