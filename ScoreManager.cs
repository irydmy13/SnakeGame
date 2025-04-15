using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ScoreManager
{
    private const string FilePath = "results.txt";

    public void SaveScore(string name, int score)
    {
        File.AppendAllText(FilePath, $"{name}:{score}\n");
    }

    public List<(string Name, int Score)> LoadScores()
    {
        var list = new List<(string, int)>();
        if (!File.Exists(FilePath)) return list;

        foreach (var line in File.ReadAllLines(FilePath))
        {
            var parts = line.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                list.Add((parts[0], score));
        }

        return list.OrderByDescending(x => x.Score).ToList();
    }
}
