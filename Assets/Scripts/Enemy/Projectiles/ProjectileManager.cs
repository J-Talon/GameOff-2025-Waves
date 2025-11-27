using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
}
