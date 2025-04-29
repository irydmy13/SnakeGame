namespace SnakeGame
{
    public class Player
    {
        public string Name { get; private set; }

        public Player(string name)
        {
            if (name.Length < 3)
                throw new System.ArgumentException("Имя должно содержать минимум 3 символа.");
            Name = name;
        }
    }
}
