using Effect.Behaviour;
using UnityEngine;

public class RangedAttack : IBehaviour
{
    private Enemy enemy;
    private ProjectileManager projectileManager;
    public void Register(Enemy enemy)
    {
        this.enemy = enemy;
        projectileManager = GameObject.FindWithTag("GameController").GetComponent<ProjectileManager>();
    }

    public void DeRegister(Enemy enemy)
    {
        this.enemy = null;
    }
    public void tick(Vector3 playerPosition)
    {
        Vector3 direction = playerPosition - enemy.transform.position;
        if (direction.magnitude >= 4)
            return;

        if (enemy.attackTimer <= enemy.attackRate)
            return;
        
        
        projectileManager.straightShot(enemy);
        enemy.attackTimer = 0;
        
        Animator anim = enemy.GetComponent<Animator>();
        anim.SetTrigger(EntityAnimatorState.ATTACK.value);


    }
}
