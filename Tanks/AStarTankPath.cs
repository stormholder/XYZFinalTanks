using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks.Pathfinding;

namespace XYZFinalTanks.Tanks;

internal class AStarTankPath(TankGameState state) : AStar, IRenderable
{
    private TankGameState state = state;
    private List<Node>? path = null;

    public IEnumerable<Node>? GetPath(Cell from)
    {
        path ??= GetNewPath(from);
        return path;
    }

    public override bool IsValid(int x, int y)
    {
        Cell newPosition = new Cell(x, y);
        return state.Map.IsValid(newPosition) &&
            state.EntityPool.GetEntityCollision(newPosition, state.Map.Walls) == null;
    }

    public List<Node>? GetNewPath(Cell from)
    {
        Cell? destination = null;
        while (destination == null)
            destination = state.EntityPool.GetRandomValidPoint();
        return FindPath(from, (Cell)destination);
    }

    public override Cell? GetNextCellFrom(Cell from)
    {
        var found = false;
        var nodes = GetPath(from);
        if (nodes == null)
            return from;
        foreach (var node in nodes)
        {
            if (found)
            {
                return node.Position;
            }
            found = node.Position.X == from.X && node.Position.Y == from.Y;
        }

        path = null;
        return from;
    }

    public void Render(IRenderer renderer)
    {
        if (path == null || path.Count <= 1)
            return;
        for (int k = 1; k < path.Count; k++)
        {
            renderer.SetPixel(
                path[k].Position.X * 4,
                path[k].Position.Y * 2,
                'x',
                2);
        }
    }

    public override void Reset()
    {
        path = null;
    }
}
