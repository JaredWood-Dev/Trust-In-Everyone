using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AllyData", menuName = "Scriptable Objects/AllyData")]
public class AllyData : ScriptableObject
{
    public string allyName;
    [Header("Cosmetics")] 
    public Color allyColor;
    public Sprite allyIcon;
    public string species;
    [Header("Combat")] 
    public string attackName;
    public int initialDamage;
    [NonSerialized]
    public int damage;
    public DamageTypes damageType;
    public float initialAttackSpeed;
    [NonSerialized]
    public float attackSpeed;
    [Header("AI Configurations")] 
    public float aggressionDistance;
    public float attackDistance;
    [Header("Movement")] 
    public float initialMoveSpeed;
    [NonSerialized]
    public float moveSpeed;
    [Header("Health")]
    public int initialHealth;
    [NonSerialized]
    public int health;
    public int initialRegen;
    [NonSerialized]
    public int regen;
}
