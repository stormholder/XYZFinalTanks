using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class Bullet : EntityBase
{
    private float _timeToMove = 0f;
    private Direction _direction;
    const int Tick = 2;
    private readonly char[,] bullet = new[,]
    {
        { ' ', 'º', ' ', ' ' },
        { ' ', ' ', ' ', ' ' }
    };

    public Bullet(Cell position, Direction direction)
    {
        Position = position;
        _direction = direction;
    }

    public override void Render(IRenderer renderer)
    {
        if (IsDisposed || 
            Position.X < 0 || 
            Position.Y < 0 || 
            Position.X >= renderer.GetWidth() || 
            Position.Y >= renderer.GetHeight())
            return;
        for (int i = 0; i < Position.Height; i++)
        {
            for (int j = 0; j < Position.Width; j++)
            {
                renderer.SetPixel(Position.X * Position.Width + j, Position.Y * Position.Height + i, bullet[i, j], 2);
            }
        }
    }

    public void Shift(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                Position = new Cell(Position.X, Position.Y - 1);
                break;
            case Direction.Down:
                Position = new Cell(Position.X, Position.Y + 1);
                break;
            case Direction.Left:
                Position = new Cell(Position.X - 1, Position.Y);
                break;
            case Direction.Right:
                Position = new Cell(Position.X + 1, Position.Y);
                break;
            default:
                break;
        };
    }

    public override void Update(float deltaTime)
    {
        if (!IsDisposed)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f)
                return;
            _timeToMove = 1 / Tick;
            Shift(_direction);
        }
    }
}
