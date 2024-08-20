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
        renderer.SetPixel(Position.X, Position.Y, symbol, 3);
    }

    public override void Update(float deltaTime)
    {
    }
}
