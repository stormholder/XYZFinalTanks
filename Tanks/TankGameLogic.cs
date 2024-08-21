using XYZFinalTanks.Shared;
using XYZFinalTanks.Shared.State;

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
        _state.EntityPool.SetMap(_state.Map);
        _state.FieldHeight = screenHeight;
        _state.FieldWidth = screenWidth;
        ChangeState(_state);
        _state.Reset();
    }
    public override void OnArrowDown()
    {
        if (currentState != _state) return;
        _state.Player.TryMoveDown(_state.Map);
    }

    public override void OnArrowLeft()
    {
        if (currentState != _state) return;
        _state.Player.TryMoveLeft(_state.Map);
    }

    public override void OnArrowRight()
    {
        if (currentState != _state) return;
        _state.Player.TryMoveRight(_state.Map);
    }

    public override void OnArrowUp()
    {
        if (currentState != _state) return;
        _state.Player.TryMoveUp(_state.Map);
    }

    public override void OnEsc()
    {
        if (currentState != _state) return;
    }

    public override void OnShoot()
    {
        if (currentState != _state) return;
        var newBullet = _state.Player.Shoot();
        if (newBullet != null)
            _state.EntityPool.AddBullet(newBullet);
            //_state.Bullets.Add(newBullet);
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
