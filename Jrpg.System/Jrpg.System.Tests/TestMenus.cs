using System.Collections.Generic;
using Xunit;
using Jrpg.MenuSystem;
using Xunit.Abstractions;

namespace Jrpg.System.Tests
{
    public class TestMenus
    {
        private ITestOutputHelper output;

        private MenuStack menuStack;

        public TestMenus(ITestOutputHelper output)
        {
            this.output = output;

            MenuStack menuStack = new MenuStack();

            Menu m1 = new Menu();
            m1.Key = "menu-party";
            m1.Location = new TilePoint(1, 2);
            m1.Size = new MenuSize(50, 30);

            Menu m2 = new Menu();
            m2.Key = "menu-options";
            m2.Location = new TilePoint(38, 1);
            m2.Size = new MenuSize(10, 20);

            Menu m3 = new Menu();
            m3.Key = "menu-time";
            m3.Location = new TilePoint(38, 25);
            m3.Size = new MenuSize(10, 4);

            Menu m4 = new Menu();
            m4.Key = "menu-location";
            m4.Location = new TilePoint(30, 31);
            m4.Size = new MenuSize(25, 1);

            menuStack.Push(m1);
            menuStack.Push(m2);
            menuStack.Push(m3);
            menuStack.Push(m4);

            this.menuStack = menuStack;
        }

        [Fact]
        public void TestMenuStack()
        {
            List<string> keys = new List<string> {
                "menu-party",
                "menu-options",
                "menu-time",
                "menu-location"
            };

            Assert.Equal(keys.Count, menuStack.Count());

            int i = 0;
            while(menuStack.HasNext())
            {
                Assert.Equal(menuStack.Next().Key, keys[i]);
                i++;
            }
        }

        [Fact]
        public void TestMenuStackPeek()
        {
            var m = menuStack.Peek();

            Assert.Equal("menu-location", m.Key);
            Assert.Equal(4, menuStack.Count());
        }

        [Fact]
        public void TestMenuStackPop()
        {
            menuStack.Pop();
            var m = menuStack.Pop();

            Assert.Equal("menu-time", m.Key);
            Assert.Equal(2, menuStack.Count());
        }

        [Fact]
        public void TestMenuStackClear()
        {
            menuStack.Clear();

            Assert.Equal(0, menuStack.Count());
        }

        [Fact]
        public void TestMenuWithContent()
        {
            var mainCharacterName = "Terry Token";
            var gameStore = GameStore.GetInstance();
            gameStore.Put<string>("MainCharacterName", mainCharacterName);

            Menu m = new Menu();

            var line1 = "Hello! $NAME$, is $FOOD$ good?";
            MenuContentToken mcLine1 = new MenuContentToken(GameStore.GetInstance());
            mcLine1.Key = "line-1";
            mcLine1.Size = new MenuSize(line1.Length, 1);
            mcLine1.Content = line1;
            mcLine1.Location = new TilePoint(1, 1);

            var replacer1 = new MenuContentToken.MenuContentTokenReplacementDefinition("$FOOD$", "Jrpg.SampleGame.Menus.Tokens.MenuTokenReplacerFoodTest, Jrpg.SampleGame");
            var replacer2 = new MenuContentToken.MenuContentTokenReplacementDefinition("$NAME$", "Jrpg.SampleGame.Menus.Tokens.MenuTokenReplacerNameTest, Jrpg.SampleGame");

            mcLine1.Replacers = new List<MenuContentToken.MenuContentTokenReplacementDefinition> {
                replacer1,
                replacer2
            };
            mcLine1.Replace();

            var line2 = "Yes";
            MenuContentOption mcLine2 = new MenuContentOption(GameStore.GetInstance());
            mcLine2.Key = "line-2";
            mcLine2.Size = new MenuSize(line2.Length, 1);
            mcLine2.Location = new TilePoint(4, 2);
            mcLine2.Content = line2;
            mcLine2.Handler = "Jrpg.SampleGame.Menus.Options.MenuOptionHandlerYesTest, Jrpg.SampleGame";

            var line3 = "No";
            MenuContentOption mcLine3 = new MenuContentOption(GameStore.GetInstance());
            mcLine3.Key = "line-3";
            mcLine3.Size = new MenuSize(line3.Length, 1);
            mcLine3.Location = new TilePoint(4, 3);
            mcLine3.Content = line3;
            mcLine3.Handler = "Jrpg.SampleGame.Menus.Options.MenuOptionHandlerNoTest, Jrpg.SampleGame";


            m.AddContent(mcLine1);
            m.AddContent(mcLine2);
            m.AddContent(mcLine3);

            var rendered = m.DebugRender();

            this.output.WriteLine(rendered);

            Assert.Contains(mainCharacterName, rendered);

            this.output.WriteLine("Choosing the NO option");

            Assert.Null(gameStore.Get<string>("OptionResult"));

            ((MenuContentOption)m.GetContent("line-3")).Handle();

            Assert.Equal("NO", gameStore.Get<string>("OptionResult"));
        }
    }
}
