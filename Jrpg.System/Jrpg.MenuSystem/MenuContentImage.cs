using Jrpg.System;
using System;
using UnityEngine;

namespace Jrpg.MenuSystem
{
    public class MenuContentImage : MenuContent
    {
        public string Content { get; set; }

        public MenuContentImage(GameStore g) : base(g) { }

        public override void Render(MonoBehaviour mono)
        {
            throw new NotImplementedException();
        }
    }
}
