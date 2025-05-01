using System;
using System.IO;
using System.Linq;

class ScoreManager
{
    const string FilePath = "results.txt";

    // Сохраняем имя и очки
    public static void SaveScore(string name, int score)
    {
        File.AppendAllText(FilePath, $"{name}:{score}\n");
    }

    // Показываем топ-5 игроков
    public static void ShowTopScores()
    {
        Console.WriteLine("\nТоп игроков:");

        var lines = File.ReadAllLines(FilePath);
        var scores = lines
            .Select(line =>
            {
                var parts = line.Split(':');
                return (Name: parts[0], Score: int.Parse(parts[1]));
            })
            .OrderByDescending(s => s.Score)
            .Take(5);

        foreach (var s in scores)
        {
            Console.WriteLine($"{s.Name} — {s.Score}");
        }
    }
}
