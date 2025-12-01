using System.Collections;
using System.Collections.Generic;
using Effect.Behaviour;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : GameEntity
{
    private List<IBehaviour> behaviours = new List<IBehaviour>();
    private CircleCollider2D circleCollider;
    private bool hasMeleeBehaviour = false;
    private Animator anim;

    public int points;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        anim = GetComponent<Animator>();
=======
        circleCollider = GetComponent<CircleCollider2D>();
>>>>>>> development
        rb.freezeRotation = true;
        
    }

    public void AddBehaviour(IBehaviour behaviour)
    {
        behaviours.Add(behaviour);
        behaviour.Register(this);
        if (behaviour is MeleeMovement)
            hasMeleeBehaviour = true;
        
        Debug.Log("behaviour added");
    }

    public void RemoveBehaviour(IBehaviour behaviour)
    {
        behaviour.DeRegister(this);
        behaviours.Remove(behaviour);
        if (behaviour is MeleeMovement)
            hasMeleeBehaviour = false;
        
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
        
        if (hasMeleeBehaviour && anim)
            anim.SetTrigger(EntityAnimatorState.ATTACK.value);
            
            
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


    public void takeDamage(float dmg, float atkFrequency)
    {
        Debug.Log("damage: " + dmg + "health: " + health);
        health -= dmg;
        if (health < 0)
        {
            if (SceneManager.GetActiveScene().name == "Temporary")
            {
                if (this.transform.position != null)
                {
                    int random = Random.Range(1, 10);
                    if (random < 4)
                    {
                        Powerup powerup = new Powerup();
                        if (random == 1)
                        {
                            powerup = EntityFactory.createHP();
                        }
                        else if (random == 2)
                        {
                            powerup = EntityFactory.createEnergy();
                        }
                        else if (random == 3)
                        {
                            powerup = EntityFactory.createHybrid();
                        }
                        powerup.transform.position = this.transform.position;
                    }
                }
            }

            //Player.Instance.GainEnergy(100);
            ScoreManager.Instance.InvokeScoreEvent(points);
            Destroy(gameObject);
        }
    }
}
