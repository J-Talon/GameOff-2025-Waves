using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField]
    public PlayerData playerData { get; private set; }
    [SerializeField]
    public EnemyData enemyData { get; private set; }
    [SerializeField]
    public WeaponData weaponData { get; private set; }

    void Start()
    {

    }
}
public interface IDataUser
{
    public void SetData(GameData data);
}

[Serializable]
public class PlayerData
{
    float maxHealth;
    float currentHealth;
    float score;
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
public class WeaponData
{
    public float amplitude;
    public float frequency;
}
