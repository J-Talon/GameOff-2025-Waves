using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : GameEntity
{
    private List<IBehaviour> behaviours = new List<IBehaviour>();

    private void Start()
    {
        AddBehaviour(new RangedMovement());
        AddBehaviour(new RangedAttack());
    }

    public void AddBehaviour(IBehaviour behaviour)
    {
        UnityEngine.Debug.Log(behaviour);
        behaviours.Add(behaviour);
        behaviour.Register(this);
    }

    public void RemoveBehaviour(IBehaviour behaviour)
    {
        behaviour.DeRegister(this);
        behaviours.Remove(behaviour);
    }
    public void tick(Vector3 playerPosition)
    {
        attackTimer += Time.deltaTime;
        foreach (IBehaviour behaviour in behaviours)
        {
            behaviour.tick(playerPosition);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // Temporarily using Player as collider
        {
            takeDamage(5f, 5f);
        }
        /*Projectile temp = collision.GetComponent<Projectile>(); // WILL CHANGE TO A PROJECTILE CLASS ONCE PROJECTILES ARE CREATED
        if (temp)
        {
            damage(temp.attackDmg);
        }*/
    }
}
