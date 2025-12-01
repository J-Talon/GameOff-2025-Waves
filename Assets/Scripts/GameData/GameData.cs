using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] public int weaponCount = 5;
    [SerializeField] public List<WeaponData> weapons = new List<WeaponData>();
    private List<WeaponData> weaponTemplates = new List<WeaponData>();
    private PlayerData playerTemplate;

    private void OnValidate()
    {
        weapons = Resources.LoadAll<WeaponData>("GameData").ToList();
        playerData = Resources.Load<PlayerData>("GameData");
    }
    private void Awake()
    {
        Main.Instance.SetGameData(this);
    }
    private void Start()
    {
    }
    public void CloneDataFromScriptableObjects()
    {
        playerTemplate = Resources.Load<PlayerData>("GameData/PlayerData");
        weaponTemplates = Resources.LoadAll<WeaponData>("GameData").ToList();
    }
    public IEnumerator InitializeNewData()
    {
        var originalPlayerData = Resources.Load<PlayerData>("GameData/PlayerData");
        playerData = ScriptableObject.Instantiate(originalPlayerData);
        yield return null;

        var weaponTemplates = Resources.LoadAll<WeaponData>("GameData");
        yield return null;

        weapons.Clear();

        foreach (var template in weaponTemplates)
        {
            weapons.Add(ScriptableObject.Instantiate(template));
            yield return null;
        }

        while (weapons.Count < weaponCount)
        {
            weapons.Add(ScriptableObject.CreateInstance<WeaponData>());
            yield return null;
        }
    }
}
public interface IDataUser
{
    public void SetData(GameData data);
}

[Serializable]
[CreateAssetMenu(fileName = "NewPlayer", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject
{

    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxEnergy;
    [SerializeField] public float currentEnergy;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float frictionCoeff; // this is for knockback calculation 
    [SerializeField] public float score = 0;
}

[Serializable]
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Game/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] public float attackPeriod;  //in millis
    [SerializeField] public float expansionSpeed; // time it takes to reach max range
    [SerializeField] public float maxRange; //units
    [SerializeField] public float baseDamage;
    [SerializeField] public float currentDamage;
    [SerializeField] public float damageMultiplier;
    [SerializeField] public float baseEnergyCost;
    [SerializeField] public float currentEnergyCost;
    [SerializeField] public float energyMultiplier;
}
