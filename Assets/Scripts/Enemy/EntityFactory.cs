using UnityEngine;

static class EntityFactory
{

    private static RuntimeAnimatorController goblinAnimation = Resources.Load<RuntimeAnimatorController>("Animation/Goblin/AnimGoblin");
    private static RuntimeAnimatorController demonDogAnimation;
    
    
    public static void createZombie(Enemy enemy)
    {
        enemy.init(5, 5, 5, 5, 5);
        enemy.AddBehaviour(new MeleeMovement());
        Animator controller = enemy.gameObject.AddComponent<Animator>();
        controller.runtimeAnimatorController =  goblinAnimation;
    }

    public static void createArcher(Enemy enemy)
    {
        enemy.init(5, 5, 5, 5, 5);
        enemy.AddBehaviour(new RangedMovement());
        enemy.AddBehaviour(new RangedAttack());
        Animator controller = enemy.gameObject.AddComponent<Animator>();
        // controller.runtimeAnimatorController =  demonDogAnimation
    }
}