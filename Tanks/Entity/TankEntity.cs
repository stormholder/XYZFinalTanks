using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks.Level;

namespace XYZFinalTanks.Tanks.Entity;

internal class TankEntity : EntityBase
{

    const char body = '@';
    private Direction direction = Direction.Right;
    public Direction Direction { get { return direction; } set { direction = value; } }

    public TankEntity()
    {
        Health = 3;
    }

    protected bool TryChangePosition(Cell newPosition)
    {
        if (!LevelModel.GetInstance().Map.IsValid(newPosition))
        {
            return false;
        }
        Position = newPosition;
        return true;
    }

    public virtual bool TryMoveLeft()
    {
        var result = TryChangePosition(new Cell(Position.X - 1, Position.Y));
        if (result)
        {
            Direction = Direction.Left;
        }
        return result;
    }

    public virtual bool TryMoveRight()
    {
        var result = TryChangePosition(new Cell(Position.X + 1, Position.Y));
        if (result)
        {
            Direction = Direction.Right;
        }
        return result;
    }

    public virtual bool TryMoveUp()
    {
        var result = TryChangePosition(new Cell(Position.X, Position.Y - 1));
        if (result)
        {
            Direction = Direction.Up;
        }
        return result;
    }

    public virtual bool TryMoveDown()
    {
        var result = TryChangePosition(new Cell(Position.X, Position.Y + 1));
        if (result)
        {
            Direction = Direction.Down;
        }
        return result;
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

    public Cell GetBulletSpawnPosition() => direction switch
    {
        Direction.Left => new Cell(Position.X - 2, Position.Y),
        Direction.Right => new Cell(Position.X + 2, Position.Y),
        Direction.Down => new Cell(Position.X, Position.Y + 2),
        Direction.Up => new Cell(Position.X, Position.Y - 2),
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
            renderer.SetPixel(Position.X, Position.Y, body, 2);
            var canon = GetCanonChar(direction);
            var canonPos = GetCanonPosition();
            renderer.SetPixel(canonPos.X, canonPos.Y, canon, 2);
        }
    }

    public override void Update(float deltaTime)
    {
        //throw new NotImplementedException();
    }

    public virtual Bullet Shoot()
    {
        var bulletSpawn = GetBulletSpawnPosition();
        return BulletFactory.GetInstance().CreateBullet(bulletSpawn, direction);
    }
}
