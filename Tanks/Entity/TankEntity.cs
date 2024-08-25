using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class TankEntity : EntityBase
{
    public float _shootCooldownTime = 1f;
    public float _moveCooldownTime = 0.3f;

    private float _shootCooldown = 0f;
    private float _moveCooldown = 0f;

    protected byte color = 2;

    private readonly Dictionary<Direction, char[,]> _views = new Dictionary<Direction, char[,]>
    {
        { Direction.Up, new[,]
        {
            { '╔', '╩', '╗', ' ' },
            { '╚', '═', '╝', ' ' },
        }
        },
        { Direction.Right, new[,]
        {
            { '╔', '═', '╗', '_' },
            { '╚', '═', '╝', 'T' },
        }
        },
        { Direction.Down, new[,]
        {
            { '╔', '═', '╗', ' ' },
            { '╚', '╦', '╝', ' ' },
        }
        },
        { Direction.Left, new[,]
        {
            { '_', '╔', '═', '╗' },
            { 'T', '╚', '═', '╝' },
        }
        }
    };

    private Direction direction = Direction.Right;
    public Direction Direction { get { return direction; } set { direction = value; } }

    public TankEntity()
    {
        Health = 3;
    }


    private bool _canMove = true;
    private bool _canShoot = true;

    public bool CanShoot => _canShoot;

    protected bool TryChangePosition(Cell newPosition, TankGameState state)
    {
        return _canMove && 
            state.Map.IsValid(newPosition) && 
            state.EntityPool.HasCollisions(newPosition) == null;
    }

    public virtual bool TryMoveLeft(TankGameState state)
    {
        Direction = Direction.Left;
        bool result = TryChangePosition(new Cell(Position.X - 1, Position.Y), state);
        if (result)
        {
            Position = new Cell(Position.X - 1, Position.Y);
            _canMove = false;
            return true;
        }
        return false;
    }

    public virtual bool TryMoveRight(TankGameState state)
    {
        Direction = Direction.Right;
        bool result = TryChangePosition(new Cell(Position.X + 1, Position.Y), state);
        if (result)
        {
            Position = new Cell(Position.X + 1, Position.Y);
            _canMove = false;
            return true;
        }
        return false;
    }

    public virtual bool TryMoveUp(TankGameState state)
    {
        Direction = Direction.Up;
        bool result = TryChangePosition(new Cell(Position.X, Position.Y - 1), state);
        if (result)
        {
            Position = new Cell(Position.X, Position.Y - 1);
            _canMove = false;
            return true;
        }
        return false;
    }

    public virtual bool TryMoveDown(TankGameState state)
    {
        Direction = Direction.Down;
        bool result = TryChangePosition(new Cell(Position.X, Position.Y + 1), state);
        if (result)
        {
            Position = new Cell(Position.X, Position.Y +  1);
            _canMove = false;
            return true;
        }
        return false;
    }

    public override void Render(IRenderer renderer)
    {
        if (IsDisposed) return;
        {
            for (var x = 0; x < Position.Height; x++)
            {
                for (var y = 0; y < Position.Width; y++)
                {
                    renderer.SetPixel(Position.X * Position.Width + y, Position.Y * Position.Height + x, _views[direction][x,y], color);
                }
            }
        }
    }

    public override void Update(float deltaTime)
    {
        if (Health <= 0)
            Dispose();
        if (IsDisposed) return;
        _moveCooldown += deltaTime;
        _shootCooldown += deltaTime;
        if (_moveCooldown >= _moveCooldownTime)
        {
            _moveCooldown = 0;
            _canMove = true;
        }
        if (_shootCooldown >= _shootCooldownTime)
        {
            _shootCooldown = 0;
            _canShoot = true;
        }
    }

    public virtual Bullet? Shoot()
    {
        if (IsDisposed) return null;
        if (_canShoot)
        {
            _canShoot = false;
            return BulletFactory.GetInstance().CreateBullet(Position, direction);
        }
        return null;
    }
}
