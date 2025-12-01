using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    [SerializeField] public float id;
}
