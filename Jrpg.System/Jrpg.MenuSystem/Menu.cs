using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jrpg.MenuSystem
{
    public class Menu
    {
        private Dictionary<string, MenuContent> Contents;

        public string Key { get; set; }
        public TilePoint Location { get; set; }
        public MenuSize Size { get; set; }

        public Menu()
        {
            Contents = new Dictionary<string, MenuContent>();
        }

        public void AddContent(MenuContent mc)
        {
            Contents[mc.Key] = mc;
        }

        public MenuContent RemoveContent(string key)
        {
            var content = Contents[key];

            Contents.Remove(key);

            return content;
        }

        public MenuContent GetContent(string key)
        {
            return Contents[key];
        }

        public List<string> Keys()
        {
            return Contents.Keys.ToList();
        }

        public string DebugRender()
        {
            StringBuilder sb = new StringBuilder();
            int prevY = 0;
            foreach(var mc in Contents.Values)
            {
                for (var i = 0; i < mc.Location.Y - prevY; i++)
                {
                    sb.AppendLine();
                }
                prevY = mc.Location.Y;

                for (var i = 0; i < mc.Location.X; i++)
                {
                    sb.Append(" ");
                }
                switch (mc.Type)
                {
                    case MenuContentType.Text:
                        sb.Append(((MenuContentText)mc).Content);
                        break;
                    case MenuContentType.Option:
                        sb.Append(((MenuContentOption)mc).Content);
                        break;
                    case MenuContentType.Token:
                        ((MenuContentToken)mc).Replace();
                        sb.Append(((MenuContentToken)mc).Content);
                        break;
                    case MenuContentType.Image:
                        sb.Append(((MenuContentImage)mc).Content);
                        break;
                    default:
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
