using Jrpg.MenuSystem;

namespace Jrpg.SampleGame.Menus.Tokens
{
    public class MenuTokenReplacerFoodTest : MenuContentTokenReplacer
    {
        public MenuTokenReplacerFoodTest() : base()
        {
            Token = "$FOOD$";
        }

        public override string Replace(Jrpg.System.GameStore g)
        {
            return "Pie";
        }
    }
}
