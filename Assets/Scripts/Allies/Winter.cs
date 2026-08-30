using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Winter : MonoBehaviour, IAlly
{
    public int Damage { get; set; }
    public DamageTypes DamageType { get; set; }
    
    public float AttackSpeed { get; set; }
    public AudioClip AttackSound;
    public float knockback;
    public float projectileSpeed;
    private AudioSource _audioSource;

    public GameObject projectile;
    
    public Attack CharacterAttack { get; set; }
    
    //Winter's attack is firing an ice bolt
    
    public void Attack()
    {
        if (_audioSource)
        {
            _audioSource.clip = AttackSound;
            float defaultPitch = 1;
            float randomPitch = UnityEngine.Random.Range(-0.2f, 0.2f);
            _audioSource.pitch = defaultPitch + randomPitch;
            _audioSource.Play();
        }
        GameObject target = GameObjectLocator.FindNearestWithTag(gameObject, "Enemy");
        
        Vector2 diff = target.transform.position - transform.position;
        
        Vector2 projectileVelocity = diff.normalized * projectileSpeed;
        
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().linearVelocity = projectileVelocity;
        AlliedProjectile projectileScript = proj.GetComponent<AlliedProjectile>();
        projectileScript.damage = Damage;
        projectileScript.damageType = DamageType;
        projectileScript.knockback = knockback;
        projectileScript.velocity = projectileVelocity;
        projectileScript.attacker = gameObject;
    }
    
    void Start()
    {
        CharacterAttack = new WinterAttack(gameObject.GetComponent<AlliedAI>());
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
}
