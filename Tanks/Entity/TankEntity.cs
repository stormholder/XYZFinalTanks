using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class TankEntity : EntityBase
{
    public float _shootCooldownTime = 1f;
    public float _moveCooldownTime = 0.3f;

    private float _shootCooldown = 0f;
    private float _moveCooldown = 0f;

    protected byte color = 2;
    protected TankGameState state;

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

    protected Direction direction = Direction.Right;
    public Direction Direction { get { return direction; } set { direction = value; } }

    public TankEntity(TankGameState state)
    {
        this.state = state;
        Health = 3;
    }


    protected bool canMove = true;
    protected bool canShoot = true;

    public bool CanShoot => canShoot;

    protected bool TryChangePosition(Cell newPosition)
    {
        return canMove && 
            state.Map.IsValid(newPosition) && 
            state.EntityPool.HasCollisions(newPosition) == null;
    }

    public virtual bool TryMoveLeft()
    {
        Direction = Direction.Left;
        bool result = TryChangePosition(new Cell(Position.X - 1, Position.Y));
        if (result)
        {
            Position = new Cell(Position.X - 1, Position.Y);
            canMove = false;
            return true;
        }
        return false;
    }

    public virtual bool TryMoveRight()
    {
        Direction = Direction.Right;
        bool result = TryChangePosition(new Cell(Position.X + 1, Position.Y));
        if (result)
        {
            Position = new Cell(Position.X + 1, Position.Y);
            canMove = false;
            return true;
        }
        return false;
    }

    public virtual bool TryMoveUp()
    {
        Direction = Direction.Up;
        bool result = TryChangePosition(new Cell(Position.X, Position.Y - 1));
        if (result)
        {
            Position = new Cell(Position.X, Position.Y - 1);
            canMove = false;
            return true;
        }
        return false;
    }

    public virtual bool TryMoveDown()
    {
        Direction = Direction.Down;
        bool result = TryChangePosition(new Cell(Position.X, Position.Y + 1));
        if (result)
        {
            Position = new Cell(Position.X, Position.Y +  1);
            canMove = false;
            return true;
        }
        return false;
    }

    public override void Render(IRenderer renderer)
    {
        if (IsDisposed) return;
        for (var x = 0; x < Position.Height; x++)
        {
            for (var y = 0; y < Position.Width; y++)
            {
                renderer.SetPixel(Position.X * Position.Width + y, Position.Y * Position.Height + x, _views[direction][x,y], color);
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
            canMove = true;
        }
        if (_shootCooldown >= _shootCooldownTime)
        {
            _shootCooldown = 0;
            canShoot = true;
        }
    }

    public virtual Bullet? Shoot()
    {
        if (IsDisposed) return null;
        if (canShoot)
        {
            canShoot = false;
            return BulletFactory.GetInstance().CreateBullet(Position, direction);
        }
        return null;
    }
}
