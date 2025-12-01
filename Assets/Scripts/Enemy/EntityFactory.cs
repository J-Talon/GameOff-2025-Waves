using UnityEngine;

static class EntityFactory
{
    private static Enemy enemy = Resources.Load<Enemy>("Prefab/Enemy");
    private static Projectile projectile = Resources.Load<Projectile>("Prefab/EnemyProjectile");
    private static Powerup powerup = Resources.Load<Powerup>("Prefab/Powerup");
    
    public static Enemy createGoblin(float strengthMod = 1)
    {
        Enemy goblin = Object.Instantiate(enemy);
        goblin.init(hp: 10 * strengthMod, atk: 5 * strengthMod, move: 4, freq: 5, atkRate: 5);
        goblin.points = (int)(10 * strengthMod);
        goblin.AddBehaviour(new MeleeMovement());
        return goblin;
    }

    public static Enemy createDog(float strengthMod = 1)
    {
        Enemy dog = Object.Instantiate(enemy);
        dog.init(hp: 10 * strengthMod, atk: 5 * strengthMod, move: 3, freq: 5, atkRate: 5);
        dog.points = (int)(15 * strengthMod);
        dog.AddBehaviour(new RangedMovement());
        dog.AddBehaviour(new RangedAttack());
        return dog;
    }

    public static Enemy createSpeedyGoblin(float strengthMod = 1)
    {
        Enemy goblin = Object.Instantiate(enemy);
        goblin.init(hp: 10 * strengthMod, atk: 5 * strengthMod, move: 20, freq: 5, atkRate: 5);
        goblin.points = (int)(10 * strengthMod);
        goblin.AddBehaviour(new MeleeMovement());
        return goblin;
    }

    public static Projectile createStraightShot()
    {
        Projectile straightShot = Object.Instantiate(projectile);
        straightShot.init(0, 5, 5, 0, 5);
        return straightShot;
    }

    public static Powerup createHP()
    {
        Powerup hp = Object.Instantiate(powerup);
        hp.AddBehaviour(new HealthPowerup());
        hp.spriteRenderer.color = Color.red;
        return hp;
    }

    public static Powerup createEnergy()
    {
        Powerup energy = Object.Instantiate(powerup);
        energy.AddBehaviour(new EnergyPowerup());
        energy.spriteRenderer.color = Color.blue;
        return energy;
    }

    public static Powerup createHybrid()
    {
        Powerup hybrid = Object.Instantiate(powerup);
        hybrid.AddBehaviour(new HealthPowerup());
        hybrid.AddBehaviour(new EnergyPowerup());
        hybrid.spriteRenderer.color = Color.green;
        return hybrid;
    }
}