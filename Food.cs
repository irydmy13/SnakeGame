using System;

class Food
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public void Spawn()
    {
        Random rnd = new Random();
        X = rnd.Next(1, Console.WindowWidth - 2);
        Y = rnd.Next(1, Console.WindowHeight - 2);

        Console.SetCursorPosition(X, Y);
        Console.Write("*");

        // Звук при появлении еды
        SoundPlayerHelper.PlayFoodSound();
    }
}
