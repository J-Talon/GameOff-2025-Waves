using TMPro;
using UnityEngine;

public class ScoreTextUpdater : MonoBehaviour, IWorker
{
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if (scoreText == null) Debug.Log("No scoreText found...");
        Initialize();
    }
    public void Initialize()
    {
        ScoreManager.OnScoreChange += UpdateBar;
        scoreText.text = $"Score: ----";
    }
    private void UpdateBar(PlayerData data, float amount)
    {
        scoreText.text = $"Score: {data.score}";
    }
}
