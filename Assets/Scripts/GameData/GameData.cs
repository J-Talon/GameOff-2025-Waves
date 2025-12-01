using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    public static GameData Instance;
    [SerializeField] public Vector2 bottomLeft;
    [SerializeField] public Vector2 topRight;
    [SerializeField] public float timerValue;
    [SerializeField] public PlayerData playerData;
    [SerializeField] public int weaponCount = 5;
    [SerializeField] public List<WeaponData> weapons = new List<WeaponData>();
    private List<WeaponData> weaponTemplates = new List<WeaponData>();
    private PlayerData playerTemplate;
    private void Awake()
    {
        Instance = this;
        Main.Instance.SetGameData(this);
        playerTemplate = Resources.Load<PlayerData>("GameData/PlayerData");
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator InitializeNewData()
    {
        Debug.Log("Init new data starting...");


        timerValue = 0;
        yield return null;
        playerData = ScriptableObject.Instantiate(playerTemplate);
        weaponTemplates = Resources.LoadAll<WeaponData>("GameData").ToList();
        yield return null;
        weapons.Clear();
        foreach (var template in weaponTemplates)
        {
            weapons.Add(ScriptableObject.Instantiate(template));
            yield return null;
        }
        Debug.Log("Init new data finished...");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 bl = new Vector3(bottomLeft.x, bottomLeft.y, 0);
        Vector3 br = new Vector3(topRight.x, bottomLeft.y, 0);
        Vector3 tr = new Vector3(topRight.x, topRight.y, 0);
        Vector3 tl = new Vector3(bottomLeft.x, topRight.y, 0);

        Gizmos.DrawLine(bl, br);
        Gizmos.DrawLine(br, tr);
        Gizmos.DrawLine(tr, tl);
        Gizmos.DrawLine(tl, bl);
    }
}
public interface IDataUser
{
    public void SetData(GameData data);
}