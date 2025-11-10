using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float health;
    public float dmg;
    public float movementSpeed;

    public bool invulnerable;

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
