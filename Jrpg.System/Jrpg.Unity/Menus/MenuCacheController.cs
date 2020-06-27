using System.Collections.Generic;
using Jrpg.MenuSystem;
using UnityEngine;

namespace Jrpg.Unity.Menus
{
    public class MenuCacheController : MonoBehaviour
    {
        private static readonly string DIALOG_PREFIX = "dialog";

        public static readonly MenuStack Menus = new MenuStack();

        private static readonly Dictionary<string, GameObject> Cache
            = new Dictionary<string, GameObject>();

        public static bool HasActiveMenus
        {
            get
            {
                return Menus.Count() > 0;
            }
        }

        public static string DialogCacheKey(string menuKey)
        {
            return $"{DIALOG_PREFIX}-{menuKey}";
        }

        public static bool MaybeUpdateCache(string menuKey)
        {
            if (Cache.ContainsKey(DialogCacheKey(menuKey)))
                return false;

            // We will need to update as there could be no entry,
            // due to being new, or stale from busting.
            Menu newMenu = MenuBuilder.BuildFromDefinition(menuKey);
            Menus.Replace(menuKey, newMenu);

            Destroy(GameObject.Find(DialogCacheKey(menuKey)));

            return true;
        }

        public static bool BustCachedMenu(string menuKey)
        {
            var builtKey = DialogCacheKey(menuKey);
            var result = false;

            if (Cache.ContainsKey(builtKey))
                result = Cache.Remove(builtKey);

            return result;
        }

        public static string Set(string menuKey, GameObject dialog)
        {
            var key = DialogCacheKey(menuKey);

            Cache.Add(key, dialog);

            return key;
        }
    }

}
