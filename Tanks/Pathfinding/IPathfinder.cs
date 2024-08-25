namespace XYZFinalTanks.Tanks.Pathfinding;

internal interface IPathfinder
{
    public List<Node>? FindPath(Cell from, Cell to);

    public Cell? GetNextCellFrom(Cell from);
    public bool IsValid(int x, int y);
    public void Reset();
}
