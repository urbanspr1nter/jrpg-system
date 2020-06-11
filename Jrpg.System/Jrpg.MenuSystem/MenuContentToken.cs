using NetStandardSystem = System;
using System.Collections.Generic;
using Jrpg.System;
using System;

namespace Jrpg.MenuSystem
{
    public class MenuContentToken : MenuContent
    {
        public class MenuContentTokenReplacementDefinition
        {
            public string Token { get; set; }
            public string Agent { get; set; }

            public MenuContentTokenReplacementDefinition(string token, string agent)
            {
                Token = token;
                Agent = agent;
            }
        }

        public MenuContentToken(GameStore g) : base(g)
        {
            Type = MenuContentType.Token;
            Replacers = new List<MenuContentTokenReplacementDefinition>();
        }
        public string Content { get; set; }
        public List<MenuContentTokenReplacementDefinition> Replacers { get; set; }

        public void Replace()
        {
            foreach(var replacer in Replacers)
            {
                try
                {
                    MenuContentTokenReplacer replaceHandler =
                        (MenuContentTokenReplacer)NetStandardSystem.Activator.CreateInstance(
                            NetStandardSystem.Type.GetType(replacer.Agent),
                            new object[] { replacer.Token }
                        );

                    Content = Content.Replace(replaceHandler.Token, replaceHandler.Replace(this.gameStore));
                }
                catch
                {
                    Console.WriteLine($"Invalid replacer {replacer.Agent} for token {replacer.Token}");
                }

            }
        }
    }
}
