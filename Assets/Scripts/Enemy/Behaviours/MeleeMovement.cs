using UnityEngine;

public class MeleeMovement : IBehaviour
{
    private GameEntity enemy;
    public void Register(GameEntity enemy)
    {
        this.enemy = enemy;
    }

    public void DeRegister(GameEntity enemy) 
    {
        this.enemy = null;
    }
    public void tick(Vector3 playerPosition)
    {
        Vector3 direction = playerPosition - enemy.transform.position;

        if (direction.magnitude > 0.5)
        {
            direction = direction.normalized;
            enemy.rb.linearVelocity = new Vector2(direction.x * enemy.moveSpeed, direction.y * enemy.moveSpeed);
        } else
        {
            enemy.rb.linearVelocity = Vector2.zero;
        }

        // IF FACING PERFORMANCE ISSUES, TAKE OUT RIGIDBODY AND USE POSITION TO MOVE
    }
}
