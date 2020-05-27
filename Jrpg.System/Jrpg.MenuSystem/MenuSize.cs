namespace Jrpg.MenuSystem
{
    public class MenuSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public MenuSize(int w, int h)
        {
            Width = w;
            Height = h;
        }

        public override string ToString()
        {
            return $"{Width} x {Height}";
        }
    }
}
