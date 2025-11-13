using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
    public float health { get; protected set; }
    public float attackDmg { get; protected set; }
    protected float movementSpeed;

    protected bool invulnerable;

    public abstract void Attack();

    public bool damage(float damage)
    {
        if (invulnerable)
        {
            return false;
        }
        health = Mathf.Max(0, health - damage);

        if (health == 0)
        {
            Destroy(gameObject);
        }
        return true;
    }
}
