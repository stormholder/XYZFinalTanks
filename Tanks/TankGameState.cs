using XYZFinalTanks.Shared;
using XYZFinalTanks.Shared.State;
using XYZFinalTanks.Tanks.Entity;

namespace XYZFinalTanks.Tanks;

internal class TankGameState : GameStateBase, IDisposable
{
    public int FieldWidth { get; set; }
    public int FieldHeight { get; set; }
    public int Level { get; set; }

    public int Score { get; set; } = 0;
    public bool GameOver { get; private set; }
    public bool HasWon { get; private set; }
    public Map Map { get; set; }

    public PlayerEntity Player = new();
    private EntityPool _entityPool = new();
    public EntityPool EntityPool => _entityPool;

    private void OnKill()
    {
        Score = Score + 1;
        HasWon = (Score == Level);
    }

    public TankGameState()
    {
        _entityPool.OnKill += OnKill;
    }

    private void RenderState(IRenderer renderer)
    {
        int MaxWidth = 24;
        var color = ConsoleColor.White;
        renderer.DrawString($"╔══════════════════════╗", Map.Width * 4 + 1, 1, color);
        renderer.DrawString($"║ Level: {Level}", Map.Width * 4 + 1, 2, color);
        renderer.DrawString("║", Map.Width * 4 + MaxWidth, 2, color);
        renderer.DrawString($"║ Score: {Score}", Map.Width * 4 + 1, 3, color);
        renderer.DrawString("║", Map.Width * 4 + MaxWidth, 3, color);
        renderer.DrawString($"║ Health: {Player.Health}", Map.Width * 4 + 1, 4, color);
        renderer.DrawString("║", Map.Width * 4 + MaxWidth, 4, color);
        renderer.DrawString($"║ Cannon: {(Player.CanShoot ? "Ready" : "Reloading...")}", Map.Width * 4 + 1, 5, color);
        renderer.DrawString("║", Map.Width * 4 + MaxWidth, 5, color);
        renderer.DrawString($"╚══════════════════════╝", Map.Width * 4 + 1, 6, color);
    }

    public override void Render(IRenderer renderer)
    {
        Map.Render(renderer);
        Player.Render(renderer);
        _entityPool.Render(renderer);
        RenderState(renderer);    
    }

    public override bool IsDone()
    {
        return GameOver || HasWon;
    }

    public override void Reset()
    {
        GameOver = false;
        HasWon = false;
        Score = 0;
        Player.Position = new(1, 1);
        Player.Health = 3;
        TankEntity enemy = new EnemyEntity();
        enemy.Position = new(1, 9);
        enemy.Health = 3;
        _entityPool.AddTank(enemy);
    }

    public override void Update(float deltaTime)
    {
        Map.Update(deltaTime);
        Player.Update(deltaTime);
        _entityPool.Update(deltaTime);
    }

    public void Dispose()
    {
        _entityPool.OnKill -= OnKill;
    }
}
