using Jrpg.System;

namespace Jrpg.MenuSystem
{
    public abstract class MenuContent
    {
        protected GameStore gameStore;

        public MenuContent(GameStore g)
        {
            gameStore = g;
        }

        public MenuContentType Type { get; set; }
        public string Key { get; set; }
        public MenuSize Size { get; set; }

        public TilePoint Location { get; set; }
    }
}
