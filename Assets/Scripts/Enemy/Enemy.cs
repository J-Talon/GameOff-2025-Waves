using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameEntity
{
    private List<IBehaviour> behaviours = new List<IBehaviour>();

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
        foreach (IBehaviour behaviour in behaviours)
        {
            behaviour.tick(playerPosition);
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        GameObject hit = other.gameObject;
        Player player = hit.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        attackTimer += Time.deltaTime;
        if (attackTimer > attackRate) {
            player.damage(attackDmg);
            attackTimer = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hit = other.gameObject;
        Player player = hit.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        attackTimer = attackRate;
    }
}
