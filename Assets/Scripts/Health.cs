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
    private Animator _anim;
    
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
        _anim = GetComponent<Animator>();
        
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
    
    public void Damage(int damage, DamageTypes damageType, Vector2 knockback, GameObject attacker = null)
    {
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
            EventManager.CreatureHit(gameObject, attacker, damage / 2, damageType);
        }
        else
        {
            ChangeHealth(-damage);
            EventManager.CreatureHit(gameObject, attacker, damage, damageType);
        }

        if (_rb)
        {
            _rb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    public void Regenerate()
    {
        //only regen if alive
        if (health > 0)
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
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Ally"))
        {
            _anim.SetBool("isDead", true);
            gameObject.GetComponent<AlliedAI>().RequestState(States.Dead);
            gameObject.tag = "Untagged";
            health = 0;
        }
        //Destroy(gameObject);
    }

    public void Ressurect()
    {
        health = maxHealth;
        if (_anim)
        {
            _anim.SetBool("isDead", false);
        }

        gameObject.tag = "Ally";
    }
}
