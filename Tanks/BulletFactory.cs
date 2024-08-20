using XYZFinalTanks.Tanks.Entity;

namespace XYZFinalTanks.Tanks;

internal class BulletFactory
{
    private static BulletFactory? instance;
    private BulletFactory() { }

    public static BulletFactory GetInstance()
    {
        instance ??= new BulletFactory();
        return instance;
    }

    public Bullet CreateBullet(Cell position, Direction direction)
    {
        return new Bullet(position, direction);
    }
}
