using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<Projectile> projectileList = new List<Projectile>();

    public Projectile projectile;

    private void Start()
    {
        projectile = Resources.Load<Projectile>("Prefab/EnemyProjectile");
    }
    public void FixedUpdate()
    {
        foreach (Projectile p in projectileList)
        {
            if (p != null)
            {
                p.tick();
            }
        }
        projectileList.RemoveAll(item => item == null);
    }

    public void straightShot(Enemy enemy)
    {
        Projectile p = Instantiate(projectile, enemy.transform.position, enemy.transform.rotation);
        p.init(0, 5, 5, 0, 5);
        p.targetPosition = GameObject.FindWithTag("Player").transform.position;
        projectileList.Add(p);

    }
}
