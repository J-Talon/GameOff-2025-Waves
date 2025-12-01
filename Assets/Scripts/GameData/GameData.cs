using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    [SerializeField] public float timerValue;
    [SerializeField] public PlayerData playerData;
    [SerializeField] public int weaponCount = 5;
    [SerializeField] public List<WeaponData> weapons = new List<WeaponData>();
    private List<WeaponData> weaponTemplates = new List<WeaponData>();
    private PlayerData playerTemplate;

    private void OnValidate()
    {
        weapons = Resources.LoadAll<WeaponData>("GameData").ToList();
        Debug.Log(weapons.Count);
        playerData = Resources.Load<PlayerData>("GameData");
        Debug.Log(playerData);
    }
    private void Awake()
    {
        Main.Instance.SetGameData(this);
    }
    private void Start()
    {
    }
    public IEnumerator InitializeNewData()
    {
        timerValue = 0;
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