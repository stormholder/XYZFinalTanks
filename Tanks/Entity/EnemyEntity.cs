using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks.Pathfinding;

namespace XYZFinalTanks.Tanks.Entity;

internal class EnemyEntity : TankEntity
{
    IPathfinder pathfinder;
    //List<Node>? _path;
    private bool _seesPlayer = false;

    public EnemyEntity(TankGameState state) : base(state)
    {
        pathfinder = new AStarTankPath(state);
    }

    private Direction GetNewDirection(Cell nextPosition)
    {
        Direction nextDirection = direction;
        if (nextPosition.X < Position.X)
            nextDirection = Direction.Left;
        else if (nextPosition.X > Position.X)
            nextDirection = Direction.Right;
        if (nextPosition.Y < Position.Y)
            nextDirection = Direction.Up;
        else if (nextPosition.Y > Position.Y)
            nextDirection = Direction.Down;
        return nextDirection;
    }

    private Cell Shift(Direction dir, Cell pos) => dir switch
    {
        Direction.Up => new Cell(pos.X, pos.Y - 1),
        Direction.Down => new Cell(pos.X, pos.Y + 1),
        Direction.Left => new Cell(pos.X - 1, pos.Y),
        Direction.Right => new Cell(pos.X + 1, pos.Y),
        _ => pos
    };

    private void Raycast()
    {
        var c = Position;
        while (true)
        {
            c = Shift(direction, c);
            var collider = state.EntityPool.HasCollisions(c);
            if (collider == null || Position.X == collider.Position.X && Position.Y == collider.Position.Y)
                continue;
            if (collider is PlayerEntity)
            {
                _seesPlayer = true;
            }
            break;
        }
    }
    
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        Raycast();
        if (_seesPlayer)
        {
            var newBullet = Shoot();
            if (newBullet != null)
                state.EntityPool.AddBullet(newBullet);
            _seesPlayer = false;
        }
        var nextPosition = pathfinder.GetNextCellFrom(Position);
        if (nextPosition != null)
        {
            direction = GetNewDirection((Cell)nextPosition);
            bool result = TryChangePosition((Cell)nextPosition);
            if (result)
            {
                Position = (Cell)nextPosition;
                canMove = false;
            } else
            {
                if (nextPosition.Value.X != Position.X &&
                    nextPosition.Value.Y != Position.Y)
                pathfinder.Reset();
            }
        }
    }

    //public override void Render(IRenderer renderer)
    //{
    //    base.Render(renderer);
    //    pathfinder.Render(renderer);
    //}
}
