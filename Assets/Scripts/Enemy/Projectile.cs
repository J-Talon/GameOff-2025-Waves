using System.Collections;
using UnityEngine;

public class Projectile : GameEntity
{
    public Vector3 targetPosition;
    public GameObject owner;
    private void Update()
    {
        tick();
    }
    public void tick()
    {
        Vector3 direction = targetPosition - transform.position;
        direction = direction.normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject gameObject = other.gameObject;
        Player player = gameObject.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        player.damage(attackDmg);
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
