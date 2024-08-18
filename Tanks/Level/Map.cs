using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Level;

internal class Map : IRenderable
{
    private char[,] _map;

    public Map(char[,] map)
    {
        _map = map;
    }

    private byte GetColorIndex(int w, int h) => _map[h, w] switch
    {
        '▓' => 1,
        '░' => 1,
        '█' => 3,
        _ => 0,
    };

    public void Render(IRenderer renderer)
    {
        for (int h = 0; h < _map.GetLength(0); h++)
        {
            for (int w = 0; w < _map.GetLength(1); w++)
            {
                if (_map[h,w] != ' ')
                    renderer.SetPixel(w, h, _map[h, w], GetColorIndex(w, h));
            }
        }
    }
}
