using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemyList;

    public void FixedUpdate()
    {
        foreach (Enemy e in enemyList)
        {
            e.tick();
        }
    }
}
