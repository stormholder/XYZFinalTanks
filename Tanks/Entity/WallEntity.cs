using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class WallEntity : EntityBase
{
    public bool IsDisposed { get; private set; } = false;
    const byte colorIdx = 1;
    const char healthy = '▓';
    const char damaged = '░';


    public WallEntity(int x, int y)
    {
        Health = 2;
        Position = new Cell(x, y);
    }

    public override void Dispose()
    {
        IsDisposed = true;
    }

    public override void Render(IRenderer renderer)
    {
        if (Health > 0)
        {
            renderer.SetPixel(Position.X, Position.Y, Health == 2 ? healthy : damaged, colorIdx);
        }
    }

    public override void Update(float deltaTime)
    {
        if (Health <= 0)
        {
            Dispose();
            return;
        }
    }
}
