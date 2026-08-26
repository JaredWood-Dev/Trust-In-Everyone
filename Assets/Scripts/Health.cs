using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int regen;
    
    public ParticleSystem hitParticles;
    public Material hitFlash;
    private Material _defaultMaterial;
    
    
    [NonSerialized]
    public HashSet<DamageTypes> Resistances = new HashSet<DamageTypes>();
    [SerializeField]
    public List<DamageTypes> DamageResistances = new List<DamageTypes>();

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    
    void Start()
    {
        
        //Copy data from out serialized field
        Resistances.Clear();
        foreach (var type in DamageResistances)
        {
            Resistances.Add(type);            
        }
        
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        
        _defaultMaterial = GetComponent<SpriteRenderer>().material;
        
        InvokeRepeating(nameof(Regenerate), regen, regen);
    }
    
    public void ChangeHealth(int changeHealth)
    {
        if (health + changeHealth > maxHealth)
            health = maxHealth;
        else if (health + changeHealth < 0)
        {
            //TODO: REPLACE LATER
            KillCreature();
        }
        else
        {
            health += changeHealth;
        }
    }
    
    public void Damage(int damage, DamageTypes damageType, Vector2 knockback)
    {
        EventManager.CreatureHit(gameObject, null, damage, damageType);
        _sr.material = hitFlash;
        Invoke(nameof(ResetMaterial), 0.1f);

        if (hitParticles)
        {
            ParticleSystem system = Instantiate(hitParticles);
            system.transform.position = transform.position;
            
        }
        
        //If resistant, take half damage
        if (Resistances.Contains(damageType))
        {
            ChangeHealth(-(damage / 2));
        }
        else
        {
            ChangeHealth(-damage);
        }

        if (_rb)
        {
            _rb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    public void Regenerate()
    {
        ChangeHealth(1);
    }

    public void ResetMaterial()
    {
        _sr.material = _defaultMaterial;
    }

    public void KillCreature()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            EventManager.EnemyKilled(gameObject, null);
        }
        Destroy(gameObject);
    }
}
