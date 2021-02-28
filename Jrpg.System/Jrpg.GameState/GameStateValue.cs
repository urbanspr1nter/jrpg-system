namespace Jrpg.GameState
{
    public class GameStateValue
    {
        public int Value { get; private set; }
     
        public string Name { get; private set; }

        private GameStateValue(string name, int value) {
            Name = name;
            Value = value;
        }

        public static GameStateValue Create(string name, int value)
        {
            return new GameStateValue(name, value);
        }
    }
}
