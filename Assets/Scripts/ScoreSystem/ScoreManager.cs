using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IManager, IDataUser
{
    [SerializeField]
    PlayerData playerData;
    public static ScoreManager Instance;
    private List<IWorker> workers = new List<IWorker>();

    public event Action<PlayerData, float> OnScoreChange;

    private void OnEnable()
    {
        Instance = this;
    }
    private void Start()
    {
        Main.Instance.AddManager(Instance);
        OnScoreChange += UpdateScore;
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
    public void UpdateScore(PlayerData data, float changeAmount)
    {
        data.score += changeAmount;
    }
    public void InvokeScoreEvent(float changeAmount)
    {
        OnScoreChange?.Invoke(playerData, changeAmount);
    }
    void OnDestroy()
    {
        Main.Instance.RemoveManager(this);
    }
}

