using UnityEngine;

static class EntityFactory
{
    private static Enemy enemy = Resources.Load<Enemy>("Prefab/Enemy");
    private static Projectile projectile = Resources.Load<Projectile>("Prefab/EnemyProjectile");
    
    public static Enemy createGoblin(float strengthMod = 1)
    {
        Enemy goblin = Object.Instantiate(enemy);
        goblin.init(hp: 10 * strengthMod, atk: 0 * strengthMod, move: 4, freq: 5, atkRate: 5);
        goblin.AddBehaviour(new MeleeMovement());
        return goblin;
    }

    public static Enemy createDog(float strengthMod = 1)
    {
        Enemy dog = Object.Instantiate(enemy);
        dog.init(hp: 10 * strengthMod, atk: 5 * strengthMod, move: 3, freq: 5, atkRate: 5);
        dog.AddBehaviour(new RangedMovement());
        dog.AddBehaviour(new RangedAttack());
        return dog;
    }

    public static Projectile createStraightShot()
    {
        Projectile straightShot = Object.Instantiate(projectile);
        straightShot.init(0, 5, 5, 0, 5);
        return straightShot;
    }
}