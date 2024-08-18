using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class BulletEntity : IUpdateable, IRenderable, IDisposable
{
    private float _timeToMove = 0f;
    private Direction direction;
    public event Action? OnCollision;
    const int Tick = 4;
    const char bullet = '●';
    private Cell _position;

    public BulletEntity(Cell position)
    {
        _position = position;
    }

    public void Render(IRenderer renderer)
    {
       renderer.SetPixel(_position.X, _position.Y, bullet, 2);
    }

    public void Shift(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                _position.Y--;
                break;
            case Direction.Down:
                _position.Y++;
                break;
            case Direction.Left:
                _position.X--;
                break;
            case Direction.Right:
                _position.X++;
                break;
            default:
                break;
        };
    }

    public void Update(float deltaTime)
    {
        _timeToMove -= deltaTime;
        if (_timeToMove > 0f)
            return;
        _timeToMove = 1 / (Tick);
        Shift(direction);
        // TODO check collisions with EVERYTHING
    }

    public void Dispose()
    {
        // TODO
    }
}
