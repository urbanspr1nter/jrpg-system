using System.Linq;
using Jrpg.System;
using Jrpg.MenuSystem;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Jrpg.Unity.Menus
{
    public class MenuBuilder
    {
        public static Menu BuildFromDefinition(string resourceName)
        {
            var definition = ResourceHelpers.LoadStringFromResource(resourceName);

            JObject o = JObject.Parse(definition);
            JToken menuKeyToken = o.GetValue("Key");
            var menuKey = menuKeyToken.ToString();

            var menuLocationToken = o.GetValue("Location");
            var menuLocationX = menuLocationToken.Value<int>("X");
            var menuLocationY = menuLocationToken.Value<int>("Y");

            var menuSizeToken = o.GetValue("Size");
            var menuSizeWidth = menuSizeToken.Value<int>("Width");
            var menuSizeHeight = menuSizeToken.Value<int>("Height");

            Menu m = new Menu();
            m.Key = menuKey;
            m.Size = new MenuSize(menuSizeWidth, menuSizeHeight);
            m.Location = new TilePoint(menuLocationX, menuLocationY);

            var menuContents = o.GetValue("Contents").Children();
            foreach (var contentToken in menuContents)
            {
                var menuContentType = contentToken.Value<string>("Type");
                var menuContentKey = contentToken.Value<string>("Key");
                var menuContentSizeWidth = contentToken["Size"].Value<int>("Width");
                var menuContentSizeHeight = contentToken["Size"].Value<int>("Height");
                var menuContentLocationX = contentToken["Location"].Value<int>("X");
                var menuContentLocationY = contentToken["Location"].Value<int>("Y");

                if (menuContentType.Equals("TEXT"))
                {
                    var menuContentContent = contentToken.Value<string>("Content");

                    MenuContentText mcText = new MenuContentText(GameStore.GetInstance());
                    mcText.Key = menuContentKey;
                    mcText.Content = menuContentContent;
                    mcText.Size = new MenuSize(menuContentSizeWidth, menuContentSizeHeight);
                    mcText.Location = new TilePoint(menuContentLocationX, menuContentLocationY);

                    m.AddContent(mcText);
                }
                else if (menuContentType.Equals("IMAGE"))
                {
                    var menuContentContent = contentToken.Value<string>("Content");

                    MenuContentImage mcImage = new MenuContentImage(GameStore.GetInstance());
                    mcImage.Key = menuContentKey;
                    mcImage.Content = menuContentContent;
                    mcImage.Size = new MenuSize(menuContentSizeWidth, menuContentSizeHeight);
                    mcImage.Location = new TilePoint(menuContentLocationX, menuContentLocationY);

                    m.AddContent(mcImage);
                }
                else if (menuContentType.Equals("TOKEN"))
                {
                    var menuContentContent = contentToken.Value<string>("Content");

                    MenuContentToken mcToken = new MenuContentToken(GameStore.GetInstance());
                    mcToken.Key = menuContentKey;
                    mcToken.Content = menuContentContent;
                    mcToken.Size = new MenuSize(menuContentSizeWidth, menuContentSizeHeight);
                    mcToken.Location = new TilePoint(menuContentLocationX, menuContentLocationY);

                    var replacerList = (JArray)contentToken["Replacers"];
                    foreach (var replacementDef in replacerList.ToList())
                    {
                        var def = new MenuContentToken.MenuContentTokenReplacementDefinition(
                            replacementDef["Token"].Value<string>(),
                            replacementDef["Agent"].Value<string>()
                        );

                        mcToken.Replacers.Add(def);
                    }


                    m.AddContent(mcToken);
                }
                else if (menuContentType.Equals("OPTION"))
                {
                    var menuContentContent = contentToken.Value<string>("Content");

                    MenuContentOption mcOption = new MenuContentOption(GameStore.GetInstance());
                    mcOption.Key = menuContentKey;
                    mcOption.Content = menuContentContent;
                    mcOption.Size = new MenuSize(menuContentSizeWidth, menuContentSizeHeight);
                    mcOption.Location = new TilePoint(menuContentLocationX, menuContentLocationY);
                    mcOption.Index = contentToken["Index"].Value<int>();
                    mcOption.Handler = contentToken["Handler"].Value<string>(); ;

                    m.AddContent(mcOption);
                }
            }

            return m;
        }
    }

}
