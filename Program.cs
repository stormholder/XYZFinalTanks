﻿using System.IO;
using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks;
using XYZFinalTanks.Tanks.Level;

namespace XYZFinalTanks;

internal class Program
{
    const float targetFrameTime = 1f / 60f;
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");
        //const char bullet = '●';
        //const char wall = '▓';
        //const char wall_weak = '░';
        //const char water = '█';
        GameData gameData = new("Resources");
        var renderer0 = new ConsoleRenderer(gameData.Palette);
        var renderer1 = new ConsoleRenderer(gameData.Palette);
        var prevRenderer = renderer0;
        var currRenderer = renderer1;

        //gameLogic.InitializeInput(input);

        var map = new Map(gameData.LevelMaps["Level 1"]);

        var lastFrameTime = DateTime.Now;
        while (true)
        {
            var frameStartTime = DateTime.Now;
            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
            //input.Update();
            //gameLogic.DrawNewState(deltaTime, currRenderer);
            map.Render(currRenderer);
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
