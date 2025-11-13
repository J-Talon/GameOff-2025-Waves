using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    private float moveSpeed = 0.5f;

    public void init(float hp)
    {
        health = hp;
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
}
