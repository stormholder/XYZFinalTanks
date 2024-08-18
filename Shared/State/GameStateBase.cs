namespace XYZFinalTanks.Shared.State;

internal abstract class GameStateBase
{
    public abstract void Update(float deltaTime);
    public abstract void Reset();
    public abstract void Draw(ConsoleRenderer renderer);
    public abstract bool IsDone();
}
