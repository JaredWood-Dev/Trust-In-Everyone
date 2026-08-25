using System;
using UnityEngine;

public class AlliedProjectile : MonoBehaviour
{
    public int damage;
    public DamageTypes damageType;
    public Vector2 velocity;
    public float knockback;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(DestroyProjectile), 2f);
    }

    private void Update()
    {
        _rb.linearVelocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().Damage(damage, damageType, velocity.normalized * knockback);
            Destroy(gameObject);
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
