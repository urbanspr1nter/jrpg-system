namespace Jrpg.ItemComponents
{
    public class ItemName
    {
        public string Name { get; }

        public ItemName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
