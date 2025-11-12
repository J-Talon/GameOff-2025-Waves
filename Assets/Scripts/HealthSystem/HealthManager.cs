using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IManager
{
    [SerializeField]
    PlayerData playerData;

    private List<IWorker> workers = new List<IWorker>();

    public static event Action HealthIncrease;
    public static event Action HealthDecrease;

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
}

