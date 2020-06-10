using System.Collections.Generic;
using System.Linq;
namespace Jrpg.MenuSystem
{
    public class MenuStack
    {
        private Dictionary<string, Menu> Menus;
        private Stack<string> KeyStack;
        private int ValuesIndex;
        public Cursor Pointer { get; private set; }

        public MenuStack()
        {
            Menus = new Dictionary<string, Menu>();
            KeyStack = new Stack<string>();
            ValuesIndex = 0;
            Pointer = new Cursor(this);
        }

        public Menu Peek()
        {
            return Menus[KeyStack.Peek()];
        }

        public void Push(Menu m)
        {
            Menus.Add(m.Key, m);
            KeyStack.Push(m.Key);

            // If has a OPTION type, then the cursor should be visible by deafult
            var hasOption = false;
            foreach(var key in m.Keys())
            {
                MenuContent mc = m.GetContent(key);
                if (mc.Type == MenuContentType.Option)
                    hasOption = true;
            }

            Pointer.Push(new MenuContentMemory {
                Index = 0,
                MenuKey = m.Key,
                Visible = hasOption
            });
        }

        public Menu Pop()
        {
            var key = KeyStack.Pop();

            var menu = Menus[key];

            Menus.Remove(key);
            Pointer.Pop();

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
