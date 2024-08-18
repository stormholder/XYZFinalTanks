using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZFinalTanks.Tanks.Pathfinding;

internal interface IPathfinder
{
    public List<Node>? FindPath(Cell from, Cell to);
    public bool IsValid(int x, int y);
}
