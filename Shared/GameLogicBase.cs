using XYZFinalTanks.Shared.Listener;
using XYZFinalTanks.Shared.State;

namespace XYZFinalTanks.Shared;

internal abstract class GameLogicBase : IArrowListener, IEscListener, IShootListener, IUpdateable
{
    protected GameStateBase? currentState { get; private set; }
    protected float time { get; private set; }
    protected int screenWidth { get; private set; }
    protected int screenHeight { get; private set; }
    public bool CanExit { get; protected set; } = false;

    public void InitializeInput(ConsoleInput input)
    {
        input.Subscribe(this);
        input.SubscribeShoot(this);
        input.SubscribeEsc(this);
    }

    protected void ChangeState(GameStateBase? state)
    {
        currentState?.Reset();
        currentState = state;
    }

    public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
    {
        time += deltaTime;
        screenWidth = renderer.width;
        screenHeight = renderer.height;

        currentState?.Update(deltaTime);
        currentState?.Render(renderer);

        Update(deltaTime);
    }

    public abstract void OnArrowDown();

    public abstract void OnArrowLeft();

    public abstract void OnArrowRight();

    public abstract void OnArrowUp();

    public abstract void OnEsc();

    public abstract void OnShoot();

    public abstract void Update(float deltaTime);
}
