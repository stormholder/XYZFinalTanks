﻿using XYZFinalTanks.Shared;
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

        var lastFrameTime = DateTime.Now;
        while (!gameLogic.CanExit)
        {
            var frameStartTime = DateTime.Now;
            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
            input.Update();
            gameLogic.DrawNewState(deltaTime, currRenderer);
            lastFrameTime = frameStartTime;
            if (!currRenderer.Equals(prevRenderer)) currRenderer.Render(prevRenderer);

            (currRenderer, prevRenderer) = (prevRenderer, currRenderer);
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
