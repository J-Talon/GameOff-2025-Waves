using UnityEngine;

public class MeleeMovement : IBehaviour
{
    private Enemy enemy;
    public void Register(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void DeRegister(Enemy enemy) 
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
