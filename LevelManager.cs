public class LevelManager
{
    public int CurrentLevel { get; private set; } = 1;

    public void UpdateLevel(int score)
    {
        if (score >= 10) CurrentLevel = 2;
        if (score >= 20) CurrentLevel = 3;
    }

    public int GetSpeed()
    {
        return 100 - (CurrentLevel * 20); // уменьшаем интервал таймера
    }
}
