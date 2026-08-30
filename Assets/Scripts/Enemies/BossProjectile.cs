using System;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage;
    public DamageTypes damageType;
    public float timeOut;
    public GameObject attacker;
    public bool isActive = false;
    
    private Rigidbody2D _rb;
    private List<GameObject> _struckAllies = new List<GameObject>();

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Invoke("Destroy", timeOut);
    }
    private void Update()
    {
        
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.gameObject.CompareTag("Ally") || collision.gameObject.CompareTag("Player"))
            {
                if (!_struckAllies.Contains(collision.gameObject))
                {
                    collision.gameObject.GetComponent<Health>().Damage(damage, damageType, Vector2.one, attacker);
                    _struckAllies.Add(collision.gameObject);
                }
            }
        }
    }
}
