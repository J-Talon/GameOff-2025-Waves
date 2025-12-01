using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameEntity
{
    private List<IBehaviour> behaviours = new List<IBehaviour>();
    private CircleCollider2D circleCollider;

    public void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
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

    public void OnTriggerStay2D(Collider2D other)
    {
        GameObject hit = other.gameObject;
        Player player = hit.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        player.damage(attackDmg);
        circleCollider.enabled = false;
        SetInvincibility(0.3f);
    }

    public void SetInvincibility(float time)
    {
        StartCoroutine(Expire(time));
    }

    IEnumerator Expire(float time)
    {
        yield return new WaitForSeconds(time);
        circleCollider.enabled = true;
    }
}
