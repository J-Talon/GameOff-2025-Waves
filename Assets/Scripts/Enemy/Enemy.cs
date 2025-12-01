using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : GameEntity
{
    private List<IBehaviour> behaviours = new List<IBehaviour>();


    private void Start()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    public void AddBehaviour(IBehaviour behaviour)
    {
        behaviours.Add(behaviour);
        behaviour.Register(this);
        Debug.Log("behaviour added");
    }

    public void RemoveBehaviour(IBehaviour behaviour)
    {
        behaviour.DeRegister(this);
        behaviours.Remove(behaviour);
        Debug.Log("removing behaviour");
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
        
        //I presume that this is not the attack state...?
        0
        
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
