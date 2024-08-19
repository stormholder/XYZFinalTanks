using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZFinalTanks.Shared;
using XYZFinalTanks.Shared.State;

namespace XYZFinalTanks.Tanks;

internal class TankGameState : GameStateBase
{
    public int FieldWidth { get; set; }
    public int FieldHeight { get; set; }
    public int Level { get; set; }
    public bool GameOver { get; private set; }
    public bool HasWon { get; private set; }

    public override void Render(IRenderer renderer)
    {
        throw new NotImplementedException();
    }

    public override bool IsDone()
    {
        throw new NotImplementedException();
    }

    public override void Reset()
    {
        throw new NotImplementedException();
    }

    public override void Update(float deltaTime)
    {
        throw new NotImplementedException();
    }
}
