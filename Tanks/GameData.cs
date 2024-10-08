﻿namespace XYZFinalTanks.Tanks;

internal class GameData
{
    private Dictionary<int, char[,]> _levelMaps = [];
    private readonly ConsoleColor[] _palette = [
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.White,
                ConsoleColor.Blue,
                ConsoleColor.Yellow
            ];

    public Dictionary<int, char[,]> LevelMaps => _levelMaps;
    public ConsoleColor[] Palette => _palette;

    public GameData(string resourcesFolder)
    {
        LoadMaps(resourcesFolder);
    }

    private void LoadMaps(string resourcesFolder)
    {
        var ls = Directory.EnumerateFiles(resourcesFolder)
            .Where(f => f.EndsWith(".map.txt"));
        foreach (var file in ls)
        {
            var f = Path.GetFileName(file);
            if (f == null)
                continue;
            var parts = f.Replace(".map.txt", "").Split('_');
            var levelMap = ReadFileToMap(file);
            _levelMaps.Add(int.Parse(parts[1]), levelMap);
        }
    }

    private static char[,] ReadFileToMap(string filePath)
    {
        var fs = File.OpenRead(filePath);
        using var sr = new StreamReader(fs);
        List<string> lines = [];
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            lines.Add(line);
        }
        var width = lines[0].ToCharArray().Length;
        var height = lines.Count;
        char[,] map = new char[lines.Count, width];
        for (int h = 0; h < height; h++)
        {
            var pixels = lines[h].ToCharArray();
            for (var w = 0; w < width; w++)
            {
                map[h, w] = pixels[w];
            }
        }
        return map;
    }
}
