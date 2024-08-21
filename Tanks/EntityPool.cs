using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks.Entity;

namespace XYZFinalTanks.Tanks;

internal class EntityPool : IUpdateable, IRenderable
{
    public event Action? OnKill;
    public List<TankEntity> Tanks = new();
    public List<Bullet> Bullets = new();
    private Map _map;

    public void SetMap(Map map)
    {
        _map = map;
    }

    public void AddBullet(Bullet bullet)
    {
        Bullets.Add(bullet);
    }

    public void AddTank(TankEntity tank)
    {
        Tanks.Add(tank);
    }

    public void RemoveDisposedEntities()
    {
        var bulletsToDispose = Bullets.Where(b => b.IsDisposed).ToList();
        foreach (var bullet in bulletsToDispose)
        {
            Bullets.Remove(bullet);
        }
        var tanksToDispose = Tanks.Where(t => t.IsDisposed).ToList();
        foreach (var tank in tanksToDispose)
        {
            if (tank is EnemyEntity enemy)
            {
                OnKill?.Invoke();
            }
            Tanks.Remove(tank);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var bullet in Bullets)
        {
            if (!bullet.IsDisposed)
            {
                if (bullet.Position.X < 0 ||
                    bullet.Position.Y < 0 ||
                    bullet.Position.X >= _map.Width ||
                    bullet.Position.Y >= _map.Height)
                {
                    bullet.Dispose();
                }
                foreach (var wall in _map.Walls)
                {
                    if (wall.Position.X == bullet.Position.X && wall.Position.Y == bullet.Position.Y)
                    {
                        wall.Health--;
                        bullet.Dispose();
                        break;
                    }
                }
                foreach (var tank in Tanks)
                {
                    tank.Update(deltaTime);
                    if (tank.Position.X == bullet.Position.X && tank.Position.Y == bullet.Position.Y)
                    {
                        tank.Health--;
                        bullet.Dispose();
                    }
                }
                bullet.Update(deltaTime);
            }
        }
        RemoveDisposedEntities();
    }

    public void Render(IRenderer renderer)
    {
        foreach (var bullet in Bullets)
        {
            if (!bullet.IsDisposed)
                bullet.Render(renderer);
        }
        foreach (var tank in Tanks)
        {
            if (!tank.IsDisposed)
            {
                tank.Render(renderer);
            }
        }
    }
}
