using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemyList;

    private Transform player;

    public void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    public void FixedUpdate()
    {
        foreach (Enemy e in enemyList)
        {
            e.tick(player.position);
        }
    }
}
