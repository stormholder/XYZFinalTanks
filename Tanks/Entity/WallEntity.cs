using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class WallEntity : EntityBase, IUpdateable, IRenderable, IDisposable
{
    const byte colorIdx = 1;
    const char healthy = '▓';
    const char damaged = '░';


    public WallEntity(int x, int y)
    {
        Health = 2;
        Position = new Cell(x, y);
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
    }

    public void Render(IRenderer renderer)
    {
        if (Health > 0)
        {
            renderer.SetPixel(Position.X, Position.Y, Health == 2 ? healthy : damaged, colorIdx);
        }
    }

    public void Update(float deltaTime)
    {
        if (Health <= 0)
            return;
    }
}
