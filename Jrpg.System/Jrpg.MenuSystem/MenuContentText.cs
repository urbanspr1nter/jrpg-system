using System;
using Jrpg.System;
using UnityEngine;

namespace Jrpg.MenuSystem
{
    public class MenuContentText : MenuContent
    {
        public string Content { get; set; }

        public MenuContentText(GameStore g) : base(g)
        {
            Type = MenuContentType.Text;
        }

        public override void Render(MonoBehaviour mono)
        {
            throw new NotImplementedException();
        }
    }
}
