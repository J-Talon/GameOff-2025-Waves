using UnityEngine;

public class RangedMovement : IBehaviour
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
        // If enemy is over 4 units away, move towards enemy
        // Player's first attack is set to 3 range so player would need to move
        if (direction.magnitude > 4)
        {
            direction = direction.normalized;
            enemy.rb.linearVelocity = new Vector2(direction.x * enemy.moveSpeed, direction.y * enemy.moveSpeed);
        } else
        {
            enemy.rb.linearVelocity = Vector2.zero;
        }
    }
}
