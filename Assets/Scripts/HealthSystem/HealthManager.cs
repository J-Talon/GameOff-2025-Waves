using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IManager, IDataUser
{
    public static HealthManager Instance;
    [SerializeField]
    PlayerData playerData;
    private float survivalTime;
    private List<IWorker> workers = new List<IWorker>();

    public static event Action<PlayerData, float> OnHealthChange;
    public static event Action<PlayerData, float> OnTimerChange;
    private void OnEnable()
    {
        Instance = this;
    }
    private void Start()
    {
        Main.Instance.AddManager(Instance);
        OnHealthChange += UpdateHealthData;
    }
    public void Update()
    {
        survivalTime += Time.deltaTime;
        OnTimerChange?.Invoke(playerData, survivalTime);
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
        Debug.Log($"{this} has been given GameData");
    }
    public void UpdateHealthData(PlayerData data, float changeAmount)
    {
        data.currentHealth = Mathf.Clamp(data.currentHealth + changeAmount, 0f, data.maxHealth);
    }
    public void UpdateSurviveTimer(PlayerData data, float changeAmount)
    {

    }
    public void InvokeHealthEvent(float changeAmount)
    {
        OnHealthChange?.Invoke(playerData, changeAmount);
    }
    void OnDestroy()
    {
        if (Main.Instance != null)
            Main.Instance.RemoveManager(this);
    }
}

