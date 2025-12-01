using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqualizerManager : MonoBehaviour, IManager, IDataUser
{
    public static EqualizerManager Instance;
    List<Slider> sliders = new List<Slider>();

    [SerializeField] List<WeaponData> data = new List<WeaponData>();
    [SerializeField] PlayerData playerData;
    public event Action<PlayerData, float> OnEnergyChange;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Instance = this;
        Main.Instance.AddManager(this);
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Slider>(out Slider slider))
            {
                sliders.Add(slider);
            }
        }
        for (int i = 0; i < sliders.Count; i++)
        {
            int index = i;
            sliders[index].onValueChanged.AddListener(value => AdjustAttackPower(data[index], value));
            sliders[index].onValueChanged.AddListener(value => AdjustEnergyCost(data[index], value));
            sliders[index].onValueChanged.AddListener(value => AdjustSliderGain(index));
        }
    }
    private void AdjustAttackPower(WeaponData data, float amount)
    {
        data.damageMultiplier = Mathf.Lerp(0f, 10f, Mathf.InverseLerp(-12f, 12f, amount));
        data.currentDamage = data.baseDamage * data.damageMultiplier;
    }
    private void AdjustEnergyCost(WeaponData data, float amount)
    {
        data.energyMultiplier = Mathf.Lerp(0f, 10f, Mathf.InverseLerp(-12f, 12f, amount));
        data.currentEnergyCost = data.baseEnergyCost * data.energyMultiplier;
    }
    public void AdjustSliderGain(int bandIndex)
    {
        MeshGenerator.Instance.bandGains[bandIndex] = sliders[bandIndex].value;
        MeshGenerator.Instance.ApplyFiveBandEQ();
    }
    public void SetData(GameData data)
    {
        this.data = data.weapons;
        this.playerData = data.playerData;
        Debug.Log($"{this} has been given GameData");
    }
    public static void RaiseEnergyChange(PlayerData player, float cost)
    {
        Instance.OnEnergyChange?.Invoke(player, cost);
        player.currentEnergy -= cost;
        Debug.Log($"Changing energy level...");
    }
    public void Register(IWorker worker)
    {
    }

    public void Deregister(IWorker worker)
    {
    }
    void OnDestroy()
    {
        if (Main.Instance != null)
            Main.Instance.RemoveManager(this);
    }
}
