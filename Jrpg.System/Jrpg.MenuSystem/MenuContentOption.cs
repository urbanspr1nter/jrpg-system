using NetStandardSystem = System;
using System.Collections.Generic;
using System.Text;
using Jrpg.System;
using UnityEngine;

namespace Jrpg.MenuSystem
{
    public class MenuContentOption : MenuContent
    {
        public string Content { get; set; }
        public string Handler { get; set; }
        public int Index { get; set; }

        public MenuContentOption(GameStore g) : base(g) 
        {
            Type = MenuContentType.Option;
        }

        public void Handle()
        {
            MenuContentOptionHandler handler =
                (MenuContentOptionHandler)NetStandardSystem.Activator.CreateInstance(
                    NetStandardSystem.Type.GetType(Handler), 
                    new object[] { }
                );

            // Now, handle
            handler.Handle(this.gameStore);
        }

        public override void Render(MonoBehaviour mono)
        {
            throw new NetStandardSystem.NotImplementedException();
        }
    }
}
