using Effect.Behaviour;
using UnityEngine;

public class RangedAttack : IBehaviour
{
    private GameEntity enemy;
    private ProjectileManager projectileManager;
    public void Register(GameEntity enemy)
    {
        this.enemy = enemy;
        projectileManager = GameObject.FindWithTag("GameController").GetComponent<ProjectileManager>();
    }

    public void DeRegister(GameEntity enemy)
    {
        this.enemy = null;
    }
    
    
    
    public void tick(Vector3 playerPosition)
    {
        Vector3 direction = playerPosition - enemy.transform.position;
        if (direction.magnitude < 8)
        {
            if (enemy.attackTimer > enemy.attackRate)
            {
                Projectile temp = EntityFactory.createStraightShot();
                temp.transform.position = enemy.transform.position;
                temp.transform.rotation = enemy.transform.rotation;
                temp.targetPosition = playerPosition;
                projectileManager.projectileList.Add(temp);


                Animator anim = enemy.GetComponent<Animator>();
                anim.SetTrigger(EntityAnimatorState.ATTACK.value);
                
                enemy.attackTimer = 0;
            }
        }
    }
}
