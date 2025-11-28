using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Powerup : GameEntity
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetLifeTime(attackRate);
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
        foreach (IBehaviour behaviour in behaviours)
        {
            behaviour.tick();
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
