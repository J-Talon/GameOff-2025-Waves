using UnityEngine;

public class RangedMovement : IBehaviour
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
        if (direction.magnitude > 3) // If enemy is over 3 units away, move towards enemy
        {
            direction = direction.normalized;
            enemy.rb.linearVelocity = new Vector2(direction.x * enemy.moveSpeed, direction.y * enemy.moveSpeed);
        } else
        {
            enemy.rb.linearVelocity = Vector2.zero;
        }
    }
}
