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

        public int Count()
        {
            return Menus.Count;
        }

        public void Render()
        {
            foreach(var m in Menus)
            {
                Console.WriteLine($"Rendering menu {m.Key} at {m.Location}, and size {m.Size}");
                m.Render();
            }
        }
    }
}
