using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jordgar : MonoBehaviour, IAlly
{
    public int Damage { get; set; }
    public DamageTypes DamageType { get; set; }
    
    public float AttackSpeed { get; set; }
    public AudioClip AttackSound;
    public float knockback;
    public float stompRadius;

    public LayerMask targetLayers;
    public ParticleSystem stompParticles;
    private AudioSource _audioSource;
    
    public Attack CharacterAttack { get; set; }

    //Jordgar's attack is a seismic stomp

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
                h.Damage(Damage, DamageType, knockbackVector, gameObject);
            }
        }
    }
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }
}
