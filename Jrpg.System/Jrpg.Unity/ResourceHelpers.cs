using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Jrpg.Unity
{
    public class ResourceHelpers
    {
        public static string LoadStringFromResource(string path)
        {
            return Resources.Load<TextAsset>(path).text;
        }

        public static Sprite LoadSpriteFromResource(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        public static List<Sprite> LoadAllSpritesFromResource(string path)
        {
            return Resources.LoadAll<Sprite>(path).ToList();
        }
    }
}
