using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AllyData", menuName = "Scriptable Objects/AllyData")]
public class AllyData : ScriptableObject
{
    public string allyName;
    [Header("Cosmetics")] 
    public Color allyColor;
    public Sprite allyIcon;
    [Header("Combat")] 
    public int damage;
    public DamageTypes damageType;
    public float attackSpeed;
    [Header("AI Configurations")] 
    public float aggressionDistance;
    public float attackDistance;
    [Header("Movement")] 
    public float moveSpeed;
    [Header("Health")] 
    public int health;
    public int regen;
}
