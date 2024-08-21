using XYZFinalTanks.Shared;

namespace XYZFinalTanks.Tanks.Entity;

internal abstract class EntityBase : IUpdateable, IRenderable, IDisposable
{
    public bool IsDisposed { get; protected set; } = false;
    public int Health { get; set; }
    public Cell Position { get; set; }

    public virtual void Dispose()
    {
        IsDisposed = true;
    }

    public abstract void Render(IRenderer renderer);

    public abstract void Update(float deltaTime);
}
