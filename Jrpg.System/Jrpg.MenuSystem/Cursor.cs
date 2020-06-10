using System.Collections.Generic;
using System.Linq;

namespace Jrpg.MenuSystem
{
    public class Cursor
    {
        private MenuStack Menus;
        private Stack<MenuContentMemory> Memory;
        public bool Visible
        {
            get
            {
                return Peek().Visible;
            }
            set
            {
                Peek().Visible = value;
            }
        }

        public Cursor(MenuStack menus)
        {
            Menus = menus;
            Memory = new Stack<MenuContentMemory>();
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

        public void MovePrevious()
        {
            MenuContentMemory mcm = Peek();

            var optionCount = GetOptionCount();

            if (mcm.Index <= 0)
                mcm.Index = optionCount - 1;
            else
                mcm.Index--;
        }

        public void MoveNext()
        {
            MenuContentMemory mcm = Peek();

            var optionCount = GetOptionCount();

            if (mcm.Index >= optionCount - 1)
                mcm.Index = 0;
            else
                mcm.Index++;
        }

        public TilePoint CurrentLocation()
        {
            MenuContentMemory mcm = Peek();

            Menu m = Menus.Get(mcm.MenuKey);

            int index = mcm.Index;

            foreach (var key in m.Keys())
            {
                MenuContent mc = m.GetContent(key);

                if (mc.Type != MenuContentType.Option)
                    continue;

                MenuContentOption mcOption = (MenuContentOption)mc;

                if (mcOption.Index == index)
                {
                    return mcOption.Location;
                }
            }

            return m.Location;
        }

        public int CurrentIndex()
        {
            return Peek().Index;
        }

        private int GetOptionCount()
        {
            MenuContentMemory mcm = Peek();
            Menu m = Menus.Get(mcm.MenuKey);

            List<string> mKeys = m.Keys();

            var optionCount = mKeys
                .Select(k => m.GetContent(k).Type == MenuContentType.Option)
                .Where(b => b == true)
                .Count();

            return optionCount;
        }
    }
}
