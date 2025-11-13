using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : GameEntity
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    private float moveSpeed = 0.5f;

    public void init(float hp)
    {
        health = hp;
        invulnerable = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void Attack()
    {
        return;
    }

    // Update is called once per frame
    public void tick(Vector3 playerPosition)
    {
        Vector3 direction = playerPosition - transform.position;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // Temporarily using Player as collider
        {
            damage(5f);
        }
        /*Projectile temp = collision.GetComponent<Projectile>(); // WILL CHANGE TO A PROJECTILE CLASS ONCE PROJECTILES ARE CREATED
        if (temp)
        {
            damage(temp.attackDmg);
        }*/
    }
}
