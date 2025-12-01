using System;
using UnityEngine;

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