using XYZFinalTanks.Shared;
using XYZFinalTanks.Tanks.Entity;

namespace XYZFinalTanks.Tanks;

internal class EntityPool : IUpdateable, IRenderable
{
    public event Action? OnKill;
    public List<TankEntity> Tanks = new();
    public List<Bullet> Bullets = new();
    private Map _map;
    private Random _rand = new Random();

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

    private EntityBase? GetEntityCollision(Cell cell, IEnumerable<EntityBase> entities)
    {
        EntityBase? result = null;
        foreach (var entity in entities)
        {
            if (entity.Position.X == cell.X && entity.Position.Y == cell.Y)
            {
                result = entity;
                break;
            }
        }
        return result;
    }

    public EntityBase? HasCollisions(Cell cell)
    {
        EntityBase? result = GetEntityCollision(cell, _map.Walls);
        if (result == null)
            result = GetEntityCollision(cell, Tanks);
        return result;
    }

    public Cell? GetRandomValidPoint()
    {
        bool found = false;
        Cell validPoint = new(0, 0);
        for (var y = _rand.Next(0, _map.Height); y < _map.Height; y++)
        {
            if (found)
                break;
            for (var x = _rand.Next(0, _map.Width); x < _map.Width; x++)
            {
                if (found)
                    break;
                validPoint.X = x;
                validPoint.Y = y;
                found = _map.IsValid(validPoint) && 
                    HasCollisions(validPoint) == null;
            }
        }
        return found ? validPoint : null;
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
            Tanks.Remove(tank);
            OnKill?.Invoke();
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
                var e = HasCollisions(bullet.Position);
                if (e != null)
                {
                    e.Health--;
                    bullet.Dispose();
                }
                bullet.Update(deltaTime);
            }
        }
        foreach (var tank in Tanks)
            if (!tank.IsDisposed)
                tank.Update(deltaTime);
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

    public void Clear()
    {
        Bullets.Clear();
        Tanks.Clear();
    }
}
