using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZFinalTanks.Tanks.Level;

internal class LevelModel
{
    private static LevelModel? instance;
    private static Map? _map;

    private LevelModel() { }

    public static LevelModel GetInstance()
    {
        instance ??= new LevelModel();
        return instance;
    }

    public static void SetMap(char[,] map)
    {
        _map = new(map);
    }

    public Map Map => _map;
}
