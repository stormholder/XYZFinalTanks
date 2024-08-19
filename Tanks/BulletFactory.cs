using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
