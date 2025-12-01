using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Powerup : GameEntity
{
    private List<PowerupBehaviour> behaviours = new();

    public void AddBehaviour(PowerupBehaviour behaviour)
    {
        behaviours.Add(behaviour);
        behaviour.Register(this);
        Debug.Log("behaviour added");
    }

    public void RemoveBehaviour(PowerupBehaviour behaviour)
    {
        behaviour.DeRegister(this);
        behaviours.Remove(behaviour);
        Debug.Log("removing behaviour");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetLifeTime(15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hit = other.gameObject;
        Player player = hit.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        // Process effects
        foreach (PowerupBehaviour behaviour in behaviours)
        {
            behaviour.tick(player);
        }
        Destroy(gameObject);
    }

    public void SetLifeTime(float time)
    {
        StartCoroutine(Expire(time));
    }

    IEnumerator Expire(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
