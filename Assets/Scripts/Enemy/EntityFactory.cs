using UnityEngine;

static class EntityFactory
{
    static Enemy zombie = Resources.Load<Enemy>("Prefab/Enemy");
    public static Enemy createZombie(float health)
    {
        zombie.init(health);
        return zombie;
    }
}