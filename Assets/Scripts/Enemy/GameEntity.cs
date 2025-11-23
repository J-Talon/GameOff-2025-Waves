using System;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
    public float health;
    public float attackDmg;
    public float moveSpeed;
    public float targetFrequency; // The closer to the target frequency, the more damage the enemy takes

    public float attackRate; // How many seconds need to pass for an attack to happen
    public float attackTimer; // How many seconds since the last attack

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public SpriteRenderer spriteRenderer;

    public void init(float hp, float atk, float move, float freq)
    {
        health = hp;
        attackDmg = atk;
        moveSpeed = move;
        targetFrequency = freq;
    }

    public void takeDamage(float dmg, float atkFrequency)
    {
        health -= dmg - Math.Abs(targetFrequency - atkFrequency);
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
