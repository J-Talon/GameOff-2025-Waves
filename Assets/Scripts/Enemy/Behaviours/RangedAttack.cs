using UnityEngine;

public class RangedAttack : IBehaviour
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
        if (direction.magnitude < 4)
        {
            if (enemy.attackTimer > enemy.attackRate)
            {
                Debug.Log("pew");
                enemy.attackTimer = 0;
            }
        }
    }
}
