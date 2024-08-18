namespace XYZFinalTanks.Tanks;

internal class CollisionManager
{
    private static CollisionManager? instance;
    private CollisionManager() { }

    public static CollisionManager GetInstance()
    {
        instance ??= new CollisionManager();
        return instance;
    }
}
