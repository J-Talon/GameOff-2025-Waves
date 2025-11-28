using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] public EnemyData enemyData;
    [SerializeField] public int weaponCount = 5;
    [SerializeField] public List<WeaponData> weapons = new List<WeaponData>();

    private void Awake()
    {
        Main.Instance.SetGameData(this);
    }
    public void GetWeaponDataScriptableObjects()
    {
        WeaponData[] weapons = Resources.LoadAll<WeaponData>("GameData");
    }

    public void InitializeNewData()
    {
        playerData = new PlayerData();
        if (weapons.Count > 0)
        {
            weapons.Clear();
        }
        weapons = Resources.LoadAll<WeaponData>("GameData").ToList();
        for (int i = 0; i < weaponCount; i++)
        {
            weapons.Add(new WeaponData());
        }
    }
}
public interface IDataUser
{
    public void SetData(GameData data);
}

[Serializable]
public class PlayerData
{

    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float currentHealth = 100;
    [SerializeField] public float maxEnergy = 1000;
    [SerializeField] public float currentEnergy = 1000;
    [SerializeField] public float moveSpeed = 5;
    [SerializeField] public float frictionCoeff = 0.02f; // this is for knockback calculation 
    [SerializeField] public float score = 0;
}
[Serializable]
public class EnemyData
{
    float maxHealth;      //maybe use list of scriptable objects to represent each enemy variation
    float currentHealth;
    float speed;
    float resonantFrequency;
    float scoreReward;
}
[Serializable]
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Game/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] public float attackPeriod = 1000;  //in millis
    [SerializeField] public float expansionSpeed = 500; // time it takes to reach max range
    [SerializeField] public float maxRange = 5; //units
    [SerializeField] public float baseDamage = 1f;
    [SerializeField] public float currentDamage;
    [SerializeField] public float damageMultiplier = 1;
    [SerializeField] public float baseEnergyCost = 10;
    [SerializeField] public float currentEnergyCost;
    [SerializeField] public float energyMultiplier = 1;
}
