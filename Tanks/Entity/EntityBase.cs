using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks.Level;

namespace XYZFinalTanks.Tanks.Entity;

internal abstract class EntityBase
{
    public int Health { get; set; }
    public Cell Position { get; set; }

    //protected bool TryChangePosition(Cell newPosition)
    //{
    //    if (LevelModel.GetInstance().Map[newPosition.X, newPosition.Y] == '#')
    //    {
    //        return false;
    //    }
    //    //_renderer.SetCell(Position.X, Position.Y, " ");
    //    Position = newPosition;
    //    //_renderer.SetCell(Position.X, Position.Y, _symbol);
    //    return true;
    //}
}
