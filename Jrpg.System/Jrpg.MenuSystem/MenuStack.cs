using System.Collections.Generic;
using System.Linq;
namespace Jrpg.MenuSystem
{
    public class MenuStack
    {
        private Dictionary<string, Menu> Menus;
        private Stack<string> KeyStack;
        private int ValuesIndex;

        public MenuStack()
        {
            Menus = new Dictionary<string, Menu>();
            KeyStack = new Stack<string>();
            ValuesIndex = 0;
        }

        public Menu Peek()
        {
            return Menus[KeyStack.Peek()];
        }

        public void Push(Menu m)
        {
            Menus.Add(m.Key, m);
            KeyStack.Push(m.Key);
        }

        public Menu Pop()
        {
            var key = KeyStack.Pop();

            var menu = Menus[key];

            Menus.Remove(key);

            return menu;
        }

        public void Replace(string key, Menu m)
        {
            Menus[key] = m;
        }

        public void Clear()
        {
            Menus.Clear();
        }

        public Menu Get(string key)
        {
            return Menus[key];
        }

        public int Count()
        {
            return Menus.Values.Count;
        }

        public bool HasNext()
        {
            if (Menus.Values.Count == 0)
                return false;
            if (ValuesIndex == Menus.Values.Count)
                return false;

            return true;
        }

        public Menu Next()
        {
            return Menus.Values.ToList()[ValuesIndex++];
        }

        public void ResetIterator()
        {
            ValuesIndex = 0;
        }
    }
}
