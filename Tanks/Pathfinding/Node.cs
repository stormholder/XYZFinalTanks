namespace XYZFinalTanks.Tanks.Pathfinding;

internal class Node(Cell startPosition)
{
    public Cell Position = startPosition;
    public int Cost = 10;
    public int Estimate;
    public int Value;
    public Node Parent;

    public void CalculateEstimate(Cell targetPosition)
    {
        Estimate = Math.Abs(Position.X - targetPosition.X) + Math.Abs(Position.Y - targetPosition.Y);
    }

    public void CalculateValue()
    {
        Value = Cost + Estimate;
    }

    public override bool Equals(object? obj) => obj is Node node && Position.X == node.Position.X && Position.Y == node.Position.Y;
}
