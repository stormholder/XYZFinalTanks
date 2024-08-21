using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class WaterEntity : EntityBase
{
    const char symbol = '█';

    public WaterEntity(int x, int y)
    {
        Position = new Cell(x, y);
    }
    public override void Render(IRenderer renderer)
    {
        for (int i = 0; i < Position.Height; i++)
        {
            for (int j = 0; j < Position.Width; j++)
            {
                renderer.SetPixel(Position.X * Position.Width + j, Position.Y * Position.Height + i, symbol, 3);
            }
        }
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Dispose() {}
}
