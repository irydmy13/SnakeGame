using System;

public class Player
{
    public string Name { get; private set; }

    public Player(string name)
    {
        if (name.Length < 3)
            throw new ArgumentException("Имя слишком короткое");
        Name = name;
    }
}
