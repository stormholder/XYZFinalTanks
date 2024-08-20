using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class Bullet : IUpdateable, IRenderable, IDisposable
{
    private float _timeToMove = 0f;
    private Direction _direction;
    public event Action? OnCollision;
    const int Tick = 2;
    const char bullet = 'º';
    private Cell _position;
    public bool IsDisposed = false;

    public Bullet(Cell position, Direction direction)
    {
        _position = position;
        _direction = direction;
    }

    public Direction Direction => _direction;
    public Cell Position => _position;

    public void Render(IRenderer renderer)
    {
        if (_position.X < 0 || _position.Y < 0 || _position.X >= renderer.GetWidth() || _position.Y >= renderer.GetHeight())
            return;
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
        Shift(_direction);
        //if (_position.X < 0 || _position.Y < 0)
        //{
        //    Dispose();
        //}
        //var _lvl = LevelModel.GetInstance();
        //if (_position.X >= _lvl.Map.Width || _position.Y >= _lvl.Map.Height)
        //{
        //    Dispose();
        //}
        //// TODO check collisions with EVERYTHING
        //foreach (var wall in _lvl.Map.Walls)
        //{
        //    if (wall.Position.X == _position.X && wall.Position.Y == _position.Y)
        //    {
        //        //result = false;
        //        // TODO reduce wall health
        //        wall.Health--;
        //        Dispose();
        //        break;
        //    }
        //}
    }

    public void Dispose()
    {
        // TODO
        IsDisposed = true;
    }
}
