using UnityEngine;

public class EnergyMarkerUpdater : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Initialize();
    }
    public void Initialize()
    {
        EqualizerManager.OnEnergyChange += UpdateBar;
    }
    private void UpdateBar(PlayerData data, float amount)
    {
        float normalized = data.currentEnergy / data.maxEnergy;
        rectTransform.localScale = new Vector3(normalized, 1f, 1f);
    }
}
