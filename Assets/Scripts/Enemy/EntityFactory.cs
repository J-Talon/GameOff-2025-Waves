using UnityEngine;

static class EntityFactory
{
    public static void createZombie(Enemy enemy)
    {
        enemy.init(5, 5, 5, 5, 5);
        enemy.AddBehaviour(new MeleeMovement());
    }

    public static void createArcher(Enemy enemy)
    {
        enemy.init(5, 5, 5, 5, 5);
        enemy.AddBehaviour(new RangedMovement());
        enemy.AddBehaviour(new RangedAttack());
    }
}