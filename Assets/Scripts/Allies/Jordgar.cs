using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jordgar : MonoBehaviour, IAlly
{
    public int damage;
    public DamageTypes damageType;
    public float knockback;
    public float attackSpeed;
    public float AttackSpeed { get; set; }
    public float stompRadius;

    public LayerMask targetLayers;
    public ParticleSystem stompParticles;
    
    public Attack CharacterAttack { get; set; }

    //Jordgar's attack is a seismic stomp

    public void Attack()
    {
        //Easy, no directionality required
        Instantiate(stompParticles, transform.position, Quaternion.identity);
        //This one is easy, it dosen't rely on any enemy location calculations - when to stop will be handled by the AI controller
        var targets = Physics2D.OverlapCircleAll(transform.position, stompRadius, targetLayers);
        foreach (var target in targets)
        {
            Health h = target.GetComponent<Health>();
            if (h)
            {
                Vector2 knockbackVector = ((Vector2)target.transform.position - (Vector2)transform.position) * knockback;
                h.Damage(damage, damageType, knockbackVector);
            }
        }
    }
    
    void Start()
    {
        AttackSpeed = attackSpeed;
    }

    private void Update()
    {
        
    }
}
