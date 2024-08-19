using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZFinalTanks.Shared;
using XYZFinalTanks.Shared.State;
using XYZFinalTanks.Tanks.Level;

namespace XYZFinalTanks.Tanks;

internal class TankGameLogic : GameLogicBase
{
    private TankGameState _state = new();
    private bool newGamePending = false;
    private int currLevel = 0;
    private ShowTextState showTextState = new(2f);
    private GameData gameData;

    public TankGameLogic(GameData gameData)
    {
        this.gameData = gameData;
    }

    private void GotoGameOver()
    {
        currLevel = 0;
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
        _state.Map = new(gameData.LevelMaps[currLevel]);
        LevelModel.SetMap(gameData.LevelMaps[currLevel]);
        _state.FieldHeight = screenHeight;
        _state.FieldWidth = screenWidth;
        ChangeState(_state);
        _state.Reset();
    }
    public override void OnArrowDown()
    {
        if (currentState != _state) return;
        _state.Player.TryMoveDown();
    }

    public override void OnArrowLeft()
    {
        if (currentState != _state) return;
        _state.Player.TryMoveLeft();
    }

    public override void OnArrowRight()
    {
        if (currentState != _state) return;
        _state.Player.TryMoveRight();
    }

    public override void OnArrowUp()
    {
        if (currentState != _state) return;
        _state.Player.TryMoveUp();
    }

    public override void OnEsc()
    {
        if (currentState != _state) return;
    }

    public override void OnShoot()
    {
        if (currentState != _state) return;
        var newBullet = _state.Player.Shoot();
        _state.Bullets.Add(newBullet);
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
