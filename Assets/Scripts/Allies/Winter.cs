using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Winter : MonoBehaviour, IAlly
{
    public int damage;
    public DamageTypes damageType;
    public float knockback;
    public float attackSpeed;
    public float AttackSpeed { get; set; }
    public float projectileSpeed;

    public GameObject projectile;
    
    public Attack CharacterAttack { get; set; }
    
    //Winter's attack is firing an ice bolt
    
    public void Attack()
    {
        GameObject target = GameObjectLocator.FindNearestWithTag(gameObject, "Enemy");
        
        Vector2 diff = target.transform.position - transform.position;
        
        Vector2 projectileVelocity = diff.normalized * projectileSpeed;
        
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().linearVelocity = projectileVelocity;
        AlliedProjectile projectileScript = proj.GetComponent<AlliedProjectile>();
        projectileScript.damage = damage;
        projectileScript.damageType = damageType;
        projectileScript.knockback = knockback;
        projectileScript.velocity = projectileVelocity;
    }
    
    void Start()
    {
        AttackSpeed = attackSpeed;
        CharacterAttack = new WinterAttack(gameObject.GetComponent<AlliedAI>());
    }

    void Update()
    {
        
    }
}
