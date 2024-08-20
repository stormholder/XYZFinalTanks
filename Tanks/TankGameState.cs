using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZFinalTanks.Shared;
using XYZFinalTanks.Shared.State;
using XYZFinalTanks.Tanks.Entity;

namespace XYZFinalTanks.Tanks;

internal class TankGameState : GameStateBase
{
    private float _timeToMove = 0f;
    //public LevelModel LevelModel = LevelModel.GetInstance();
    public int FieldWidth { get; set; }
    public int FieldHeight { get; set; }
    public int Level { get; set; }
    public bool GameOver { get; private set; }
    public bool HasWon { get; private set; }
    public Map Map { get; set; }

    public PlayerEntity Player = new();
    public List<EnemyEntity> Enemies = new();
    public List<Bullet> Bullets = new();

    public override void Render(IRenderer renderer)
    {
        Map.Render(renderer);
        Player.Render(renderer);
        foreach (var bullet in Bullets)
        {
            if (!bullet.IsDisposed)
                bullet.Render(renderer);
        }
        //foreach (var enemy in Enemies)
        //{
        //    enemy.Render(renderer);
        //}
    }

    public override bool IsDone()
    {
        return false;
        //throw new NotImplementedException();
    }

    public override void Reset()
    {
        GameOver = false;
        HasWon = false;
        Player.Position = new(7,7);
        Player.Health = 3;
        _timeToMove = 0f;
    }

    public override void Update(float deltaTime)
    {
        Map.Update(deltaTime);
        Player.Update(deltaTime);
        var bulletsToDispose = Bullets.Where(b => b.IsDisposed).ToList();
        foreach (var bullet in bulletsToDispose)
        {
            Bullets.Remove(bullet);
        }
        
        foreach (var bullet in Bullets)
        {
            if (!bullet.IsDisposed)
            {
                if (bullet.Position.X < 0 || 
                    bullet.Position.Y < 0 || 
                    bullet.Position.X >= Map.Width || 
                    bullet.Position.Y >= Map.Height)
                {
                    bullet.Dispose();
                }
                // TODO check collisions with EVERYTHING
                foreach (var wall in Map.Walls)
                {
                    if (wall.Position.X == bullet.Position.X && wall.Position.Y == bullet.Position.Y)
                    {
                        wall.Health--;
                        bullet.Dispose();
                        break;
                    }
                }
                bullet.Update(deltaTime);
            }
        }
    }
}
