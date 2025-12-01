using TMPro;
using UnityEngine;

public class SurvivalTimeUpdater : MonoBehaviour, IWorker
{
    private TextMeshProUGUI timeText;

    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        if (timeText == null) Debug.Log("No TextMeshProUGUI found on SurvivalTimeUpdater!");
        Initialize();
    }

    public void Initialize()
    {
        HealthManager.OnTimerChange += UpdateTime;
        timeText.text = "Time: 00:00";
    }

    private void UpdateTime(PlayerData data, float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.FloorToInt(totalSeconds % 60f);

        timeText.text = $"Time: {minutes:00}:{seconds:00}";
    }
}
