using Effect.Behaviour;
using UnityEngine;

public class MeleeMovement : IBehaviour
{
    private Enemy enemy;
    private int facingDir = -1;
    private Animator animator;
    
    public void Register(Enemy enemy)
    {
        this.enemy = enemy;
        facingDir = -1;
        animator = enemy.gameObject.GetComponent<Animator>();

    }

    public void DeRegister(Enemy enemy) 
    {
        this.enemy = null;
        animator = null;
    }
    
    public void tick(Vector3 playerPosition)
    {
        Vector3 direction = playerPosition - enemy.transform.position;
        direction = direction.normalized;
        enemy.rb.linearVelocity = new Vector2(direction.x * enemy.moveSpeed, direction.y * enemy.moveSpeed);

        float magnitudeSqrd = (direction.x * direction.x) + (direction.y * direction.y);

        int targetDir = direction.x > 0 ? 1 : -1;
        Vector2 scale = enemy.transform.localScale;
        if (targetDir > 0 && facingDir < 0)
        {
            scale.x *= -1;
            facingDir = 1;
            enemy.transform.localScale = scale;
        }
        else if (targetDir < 0 && facingDir > 0)
        {
            scale.x *= -1;
            facingDir = -1;
            enemy.transform.localScale = scale;
        }

        string value = EntityAnimatorState.WALK.value;
        if (magnitudeSqrd > 0)
            animator.SetBool(value, true);
        else
            animator.SetBool(value, false);
  

    }
}
