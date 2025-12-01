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
        EqualizerManager.Instance.OnEnergyChange += UpdateBar;
    }
    private void UpdateBar(PlayerData data, float amount)
    {
        data.currentEnergy = Mathf.Clamp(data.currentEnergy, 0f, data.maxEnergy);
        float normalized = data.currentEnergy / data.maxEnergy;
        rectTransform.localScale = new Vector3(normalized, 1f, 1f);
        Debug.Log("Energy bar updated!!!");
    }
    private void OnDestroy()
    {
        if (EqualizerManager.Instance != null)
            EqualizerManager.Instance.OnEnergyChange -= UpdateBar;
    }
}
