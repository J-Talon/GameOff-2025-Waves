using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqualizerManager : MonoBehaviour, IManager, IDataUser
{
    List<Slider> sliders = new List<Slider>();

    [SerializeField] List<WeaponData> data = new List<WeaponData>();

    public static event Action<PlayerData, float> OnEnergyChange;
    private void Start()
    {
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
        }
    }
    private void AdjustAttackPower(WeaponData data, float amount)
    {
        //float offsetSliderAmount = amount - 0.5f;
        //float newDamage = data.baseDamage + (offsetSliderAmount * data.damageMultiplier) / data.baseDamage;
        data.currentDamage = data.baseDamage * data.damageMultiplier * amount;
    }
    private void AdjustEnergyCost(WeaponData data, float amount)
    {
        //float offsetSliderAmount = amount - 0.5f;
        //float newEnergyCost = data.baseEnergyCost + offsetSliderAmount * data.baseEnergyMultiplier;
        // data.currentEnergyCost = Mathf.Clamp(newEnergyCost, 0, data.maxDamage);
        data.currentEnergyCost = data.baseEnergyCost * data.energyMultiplier * amount;
    }

    public void SetData(GameData data)
    {
        this.data = data.weapons;
        Debug.Log($"{this} has been given GameData");
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
