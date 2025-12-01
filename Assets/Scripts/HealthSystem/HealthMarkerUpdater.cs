using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HealthMarkerUpdater : MonoBehaviour, IWorker
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Initialize();
    }
    public void Initialize()
    {
        HealthManager.OnHealthChange += UpdateBar;
    }
    private void UpdateBar(PlayerData data, float amount)
    {
        float normalized = data.currentHealth / data.maxHealth;
        rectTransform.localScale = new Vector3(normalized, 1f, 1f);
    }
    public void OnDestroy()
    {
        HealthManager.OnHealthChange -= UpdateBar;
    }
}
