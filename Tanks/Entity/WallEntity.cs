using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class WallEntity : EntityBase
{
    const byte colorIdx = 1;
    const char healthy = '▓';
    const char damaged = '░';


    public WallEntity(int x, int y)
    {
        Health = 2;
        Position = new Cell(x, y);
    }

    public override void Render(IRenderer renderer)
    {
        if (Health > 0)
        {
            for (int i = 0; i < Position.Height; i++)
            {
                for (int j = 0; j < Position.Width; j++)
                {
                    renderer.SetPixel(Position.X * Position.Width + j, Position.Y * Position.Height + i, Health == 2 ? healthy : damaged, colorIdx);
                }
            }
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
