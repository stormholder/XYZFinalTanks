using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks.Entity;

namespace XYZFinalTanks.Tanks;

internal class Map : IRenderable, IUpdateable
{
    private char[,] _map;
    private List<WallEntity> _walls = [];
    private List<WaterEntity> _water = [];

    public Map(char[,] map)
    {
        _map = map;
        for (int h = 0; h < _map.GetLength(0); h += 2)
        {
            for (int w = 0; w < _map.GetLength(1); w += 4)
            {
                if (_map[h, w] == '▓')
                {
                    _walls.Add(new(w / 4, h / 2));
                }
                else if (_map[h, w] == '█')
                {
                    _water.Add(new(w / 4, h / 2));
                }
            }
        }
    }
    public int Width => _map.GetLength(1);
    public int Height => _map.GetLength(0);
    public List<WallEntity> Walls => _walls;

    public bool IsValid(Cell cell)
    {
        var result = true;
        if (cell.X < 0 ||
            cell.Y < 0 ||
            cell.X >= Width ||
            cell.Y >= Height)
            return false;
        foreach (var w in _water)
        {
            if (w.Position.X == cell.X && w.Position.Y == cell.Y)
            {
                result = false;
                break;
            }
        }
        foreach (var wall in _walls)
        {
            if (wall.Position.X == cell.X && wall.Position.Y == cell.Y)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    public void Render(IRenderer renderer)
    {
        foreach (var wall in _walls)
        {
            wall.Render(renderer);
        }
        foreach (var w in _water)
        {
            w.Render(renderer);
        }
    }

    public void Update(float deltaTime)
    {
        var wallsToDispose = _walls.Where(w => w.IsDisposed).ToList();
        foreach (var wall in wallsToDispose)
        {
            _walls.Remove(wall);
        }
        foreach (var wall in _walls)
        {
            wall.Update(deltaTime);
        }
    }
}
