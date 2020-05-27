using System.Collections.Generic;

namespace Jrpg.MenuSystem
{
    public class Cursor
    {
        private Stack<MenuContentMemory> Memory;
        public bool Visible { get; set; }

        public Cursor()
        {
            Memory = new Stack<MenuContentMemory>();
        }

        public void Execute() 
        {
            return;
        }

        public MenuContentMemory Peek()
        {
            return Memory.Peek();
        }

        public void Push(MenuContentMemory mcm)
        {
            Memory.Push(mcm);
        }

        public MenuContentMemory Pop()
        {
            return Memory.Pop();
        }
    }
}
