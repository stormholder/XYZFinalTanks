namespace XYZFinalTanks.Shared.State;

internal abstract class GameStateBase : IUpdateable, IRenderable
{
    public abstract void Update(float deltaTime);
    public abstract void Reset();
    public abstract void Render(IRenderer renderer);
    public abstract bool IsDone();
}
