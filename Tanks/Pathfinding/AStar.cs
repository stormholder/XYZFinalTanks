namespace XYZFinalTanks.Tanks.Pathfinding;

internal class AStar : IPathfinder
{
    private int[] dx = { -1, 0, 1, 0 };
    private int[] dy = { 0, 1, 0, -1 };
    public List<Node>? FindPath(Cell from, Cell to)
    {
        Node startNode = new Node(from);
        Node targetNode = new Node(to);

        List<Node> openList = new() { startNode };
        List<Node> closedList = new();

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];

            foreach (var node in openList)
            {
                if (node.Value < currentNode.Value)
                {
                    currentNode = node;
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode.Position.X == targetNode.Position.X && currentNode.Position.Y == targetNode.Position.Y)
            {
                List<Node> path = [];
                while (currentNode != null)
                {
                    path.Add(currentNode);
                    currentNode = currentNode.Parent;
                }

                path.Reverse();
                return path;
            }

            for (int i = 0; i < dx.Length; i++)
            {
                int newX = currentNode.Position.X + dx[i];
                int newY = currentNode.Position.Y + dy[i];

                if (IsValid(newX, newY))
                {
                    Node neighbor = new Node(new Cell(newX, newY));
                    if (closedList.Contains(neighbor))
                        continue;
                    neighbor.Parent = currentNode;
                    neighbor.CalculateEstimate(targetNode.Position);
                    neighbor.CalculateValue();
                    openList.Add(neighbor);
                }
            }
        }
        return null;
    }

    public bool IsValid(int x, int y)
    {
        throw new NotImplementedException();
    }
}
