using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IManager, IDataUser
{
    public List<Enemy> enemyList;
    private GameData data;
    private Transform player;

    public void Start()
    {
        Main.Instance.AddManager(this);
        player = FindFirstObjectByType<Player>().transform;
    }

    public void FixedUpdate()
    {
        foreach (Enemy e in enemyList)
        {
            if (e != null)
            {
                e.tick(player.position);
            }
        }
        enemyList.RemoveAll(item => item == null);
    }

    public void SetData(GameData data)
    {
        this.data = data;
        Debug.Log($"{this} has been given GameData");
    }
    public void Register(IWorker worker)
    {
    }
    public void Deregister(IWorker worker)
    {
    }
    public void OnDestroy()
    {
        Main.Instance.RemoveManager(this);
    }
}