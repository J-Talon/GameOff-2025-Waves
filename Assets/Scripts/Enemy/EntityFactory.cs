using UnityEngine;

static class EntityFactory
{
    private static Enemy enemy = Resources.Load<Enemy>("Prefab/Enemy");
    private static Projectile projectile = Resources.Load<Projectile>("Prefab/EnemyProjectile");
    
    public static Enemy createZombie()
    {
        Enemy zombie = Object.Instantiate(enemy);
        zombie.init(5, 5, 5, 5, 5);
        zombie.AddBehaviour(new MeleeMovement());
        return zombie;
    }

    public static Enemy createArcher()
    {
        Enemy archer = Object.Instantiate(enemy);
        archer.init(100, 5, 5, 5, 5);
        archer.AddBehaviour(new RangedMovement());
        archer.AddBehaviour(new RangedAttack());
        return archer;
    }

    public static Projectile createStraightShot()
    {
        Projectile straightShot = Object.Instantiate(projectile);
        straightShot.init(0, 5, 5, 0, 5);
        return straightShot;
    }
}