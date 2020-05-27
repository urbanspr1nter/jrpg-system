using NetStandardSystem = System;
using System.Collections.Generic;
using Jrpg.System;
using UnityEngine;

namespace Jrpg.MenuSystem
{
    public class MenuContentToken : MenuContent
    {
        public MenuContentToken(GameStore g) : base(g)
        {
            Type = MenuContentType.Token;
        }
        public string Content { get; set; }
        public List<string> Replacers { get; set; }

        public void Replace()
        {
            foreach(var replacer in Replacers)
            {
                MenuContentTokenReplacer replaceHandler =
                    (MenuContentTokenReplacer)NetStandardSystem.Activator.CreateInstance(
                        NetStandardSystem.Type.GetType(replacer),
                        new object[] { }
                    );

                Content = Content.Replace(replaceHandler.Token, replaceHandler.Replace(this.gameStore));
            }
        }

        public override void Render(MonoBehaviour mono)
        {
            throw new NetStandardSystem.NotImplementedException();
        }
    }
}
