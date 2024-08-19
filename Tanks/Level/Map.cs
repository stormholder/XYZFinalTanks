using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks.Entity;

namespace XYZFinalTanks.Tanks.Level;

internal class Map : IRenderable, IUpdateable
{
    private char[,] _map;
    private List<WallEntity> _walls = new();

    public Map(char[,] map)
    {
        _map = map;
        for (int h = 0; h < _map.GetLength(0); h++)
        {
            for (int w = 0; w < _map.GetLength(1); w++)
            {
                if (_map[h, w] == '▓')
                {
                    _walls.Add(new(w, h));
                }
            }
        }
    }

    public void Render(IRenderer renderer)
    {
        for (int h = 0; h < _map.GetLength(0); h++)
        {
            for (int w = 0; w < _map.GetLength(1); w++)
            {
                if (_map[h,w] == '█')
                    renderer.SetPixel(w, h, _map[h, w], 3);
            }
        }
        foreach (var wall in _walls)
        {
            wall.Render(renderer);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var wall in _walls)
        {
            wall.Update(deltaTime);
        }
    }
}
