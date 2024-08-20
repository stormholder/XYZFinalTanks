using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal class TankEntity : EntityBase
{
    private float _shootCooldownTime = 1f;
    private float _moveCooldownTime = 0.3f;

    private float _shootCooldown = 0f;
    private float _moveCooldown = 0f;

    const char body = '@';
    char[,] bodyParts = new[,]
    {
        { '╔', '═', '╗' },
        { '║', ' ', '║' },
        { '╚', '═', '╝' },
    };
    private Direction direction = Direction.Right;
    public Direction Direction { get { return direction; } set { direction = value; } }

    public TankEntity()
    {
        Health = 3;
    }


    private bool _canMove = true;
    private bool _canShoot = true;

    protected bool TryChangePosition(Cell newPosition, Map map)
    {
        if (!_canMove)
            return false;
        if (!map.IsValid(newPosition))
            return false;
        Position = newPosition;
        _canMove = false;
        return true;
    }

    public virtual bool TryMoveLeft(Map map)
    {
        Direction = Direction.Left;
        return TryChangePosition(new Cell(Position.X - 1, Position.Y), map);
    }

    public virtual bool TryMoveRight(Map map)
    {
        Direction = Direction.Right;
        return TryChangePosition(new Cell(Position.X + 1, Position.Y), map);
    }

    public virtual bool TryMoveUp(Map map)
    {
        Direction = Direction.Up;
        return TryChangePosition(new Cell(Position.X, Position.Y - 1), map);
    }

    public virtual bool TryMoveDown(Map map)
    {
        Direction = Direction.Down;
        return TryChangePosition(new Cell(Position.X, Position.Y + 1), map);
    }

    public char GetCanonChar(Direction direction) => direction switch
    {
        Direction.Up => '▲',
        Direction.Right => '►',
        Direction.Down => '▼',
        Direction.Left => '◄',
        _ => ' '
    };
    public Cell GetCanonPosition() => direction switch
    {
        Direction.Left => new Cell(Position.X - 1, Position.Y),
        Direction.Right => new Cell(Position.X + 1, Position.Y),
        Direction.Down => new Cell(Position.X, Position.Y + 1),
        Direction.Up => new Cell(Position.X, Position.Y - 1),
        _ => Position
    };

    public override void Dispose()
    {
        //throw new NotImplementedException();
    }

    public override void Render(IRenderer renderer)
    {
        if (Health > 0)
        {
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    renderer.SetPixel(Position.X - 1 + y, Position.Y - 1 + x, bodyParts[x, y], 2);
                }
            }

            var canon = GetCanonChar(direction);
            var canonPos = GetCanonPosition();
            renderer.SetPixel(canonPos.X, canonPos.Y, canon, 2);
        }
    }

    public override void Update(float deltaTime)
    {
        //throw new NotImplementedException();
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
        if (_canShoot)
        {
            _canShoot = false;
            return BulletFactory.GetInstance().CreateBullet(Position, direction);
        }
        return null;
    }
}
