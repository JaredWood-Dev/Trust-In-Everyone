using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int regen;
    
    [NonSerialized]
    public HashSet<DamageTypes> Resistances = new HashSet<DamageTypes>();
    [SerializeField]
    public List<DamageTypes> DamageResistances = new List<DamageTypes>();

    private Rigidbody2D _rb;

    void Start()
    {
        
        //Copy data from out serialized field
        Resistances.Clear();
        foreach (var type in DamageResistances)
        {
            Resistances.Add(type);            
        }
        
        _rb = GetComponent<Rigidbody2D>();
        
        InvokeRepeating(nameof(Regenerate), regen, regen);
    }
    
    public void ChangeHealth(int changeHealth)
    {
        if (health + changeHealth > maxHealth)
            health = maxHealth;
        else if (health + changeHealth < 0)
        {
            //TODO: REPLACE LATER
            Destroy(gameObject);
        }
        else
        {
            health += changeHealth;
        }
    }
    
    public void Damage(int damage, DamageTypes damageType, Vector2 knockback)
    {
        EventManager.CreatureHit(gameObject, null, damage, damageType);
        //If resistant, take half damage
        if (Resistances.Contains(damageType))
        {
            ChangeHealth(-(damage / 2));
        }
        else
        {
            ChangeHealth(-damage);
        }

        if (_rb != null)
        {
            _rb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    public void Regenerate()
    {
        ChangeHealth(1);
    }
}
