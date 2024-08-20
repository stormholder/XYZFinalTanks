using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks;

namespace XYZFinalTanks;

internal class Program
{
    const float targetFrameTime = 1f / 60f;
    static void Main(string[] args)
    {
        GameData gameData = new("Resources");
        var input = new ConsoleInput();
        var renderer0 = new ConsoleRenderer(gameData.Palette);
        var renderer1 = new ConsoleRenderer(gameData.Palette);
        var prevRenderer = renderer0;
        var currRenderer = renderer1;
        var gameLogic = new TankGameLogic(gameData);
        gameLogic.InitializeInput(input);

        //var map = new Map(gameData.LevelMaps[1]);
        //var bullet = new Bullet(new(40, 10), Direction.Left);

        var lastFrameTime = DateTime.Now;
        while (true)
        {
            var frameStartTime = DateTime.Now;
            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
            //bullet.Update(deltaTime);
            input.Update();
            gameLogic.DrawNewState(deltaTime, currRenderer);
            //map.Render(currRenderer);
            //bullet.Render(currRenderer);
            lastFrameTime = frameStartTime;
            if (!currRenderer.Equals(prevRenderer)) currRenderer.Render();

            var tmp = prevRenderer;
            prevRenderer = currRenderer;
            currRenderer = tmp;
            currRenderer.Clear();

            var nextFrameTime = frameStartTime + TimeSpan.FromSeconds(targetFrameTime);
            var endFrameTime = DateTime.Now;
            if (nextFrameTime > endFrameTime)
            {
                Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
            }
        }
    }
}
