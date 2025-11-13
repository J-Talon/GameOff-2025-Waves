using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IManager, IDataUser
{
    [SerializeField]
    PlayerData playerData;

    private List<IWorker> workers = new List<IWorker>();

    public static event Action ScoreIncrease;

    void Start()
    {
        Main.Instance.AddManager(this);
    }
    public void Register(IWorker worker)
    {
        workers.Add(worker);
        Debug.Log($"Registered {worker} to {this}...");
    }
    public void Deregister(IWorker worker)
    {
        workers.Remove(worker);
        Debug.Log($"Deregistered {worker} from {this}...");
    }

    public void SetData(GameData data)
    {
        playerData = data.playerData;
    }

}

