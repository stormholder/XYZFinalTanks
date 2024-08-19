using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZFinalTanks.Shared;
using XYZFinalTanks.Shared.State;

namespace XYZFinalTanks.Tanks;

internal class TankGameLogic : GameLogicBase
{
    private TankGameState _state = new();
    private bool newGamePending = false;
    private int currLevel = 1;
    private ShowTextState showTextState = new(2f);
    private void GotoGameOver()
    {
        currLevel = 1;
        newGamePending = true;
        showTextState.text = $"Game Over!";
        ChangeState(showTextState);
    }
    private void GotoNextLevel()
    {
        currLevel++;
        newGamePending = false;
        showTextState.text = $"Level {currLevel}";
        ChangeState(showTextState);
    }

    public void GotoGameplay()
    {
        _state.Level = currLevel;
        _state.FieldHeight = screenHeight;
        _state.FieldWidth = screenWidth;
        ChangeState(_state);
        _state.Reset();
    }
    public override void OnArrowDown()
    {
        if (currentState != _state) return;
    }

    public override void OnArrowLeft()
    {
        if (currentState != _state) return;
    }

    public override void OnArrowRight()
    {
        if (currentState != _state) return;
    }

    public override void OnArrowUp()
    {
        if (currentState != _state) return;
    }

    public override void OnEsc()
    {
        if (currentState != _state) return;
    }

    public override void OnShoot()
    {
        if (currentState != _state) return;
    }

    public override void Update(float deltaTime)
    {
        if (currentState != null && !currentState.IsDone())
            return;
        if (currentState == null || currentState == _state && !_state.GameOver)
        {
            GotoNextLevel();
        }
        else if (currentState == _state && !_state.GameOver)
        {
            GotoGameOver();
        }
        else if (currentState != _state && newGamePending)
        {
            GotoNextLevel();
        }
        else if (currentState != _state && !newGamePending)
        {
            GotoGameplay();
        }
    }
}
