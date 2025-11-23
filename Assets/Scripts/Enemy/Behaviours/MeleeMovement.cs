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
        direction = direction.normalized;
        enemy.rb.linearVelocity = new Vector2(direction.x * enemy.moveSpeed, direction.y * enemy.moveSpeed);
    }
}
