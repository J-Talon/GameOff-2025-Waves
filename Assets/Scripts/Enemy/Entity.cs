using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected float health; 
    protected float attackDmg;
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
        return true;
    }
}
