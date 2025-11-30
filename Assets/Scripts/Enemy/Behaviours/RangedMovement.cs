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
        Debug.Log(direction.magnitude);
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
