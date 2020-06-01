using System;
using System.Collections.Generic;
using System.Linq;
namespace Jrpg.MenuSystem
{
    public class MenuStack
    {
        private Stack<Menu> Menus;

        public MenuStack()
        {
            Menus = new Stack<Menu>();
        }

        public Menu Peek()
        {
            return Menus.Peek();
        }

        public void Push(Menu m)
        {
            Menus.Push(m);
        }

        public Menu Pop()
        {
            return Menus.Pop();
        }

        public void Clear()
        {
            Menus.Clear();
        }

        public List<string> Keys()
        {
            return Menus.Select(m => m.Key).ToList();
        }

        public Menu Get(string key)
        {
            return Menus.Where(m => m.Key.Equals(key)).FirstOrDefault();
        }

        public int Count()
        {
            return Menus.Count;
        }
    }
}
