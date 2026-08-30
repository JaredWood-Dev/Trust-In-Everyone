using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Garf : MonoBehaviour, IAlly
{

    public int Damage { get; set; }
    public DamageTypes DamageType { get; set; }
    public float AttackSpeed { get; set; }
    public AudioClip AttackSound;

    //Direction to aim the bolt
    [Header("Targeting")]
    public float direction;
    public LayerMask targetLayers;
    public ParticleSystem lightningParticles;
    private AudioSource _audioSource;
    
    public Attack CharacterAttack { get; set; }

    //Garf's attack is a bolt of lightning
    
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
        //Calculate rotation based on enemies
        GameObject target = GameObjectLocator.FindNearestWithTag(gameObject, "Enemy");
        Vector2 diff = (target.transform.position - transform.position).normalized;
        direction = MathFunctions.VectorToDegrees(diff);
        
        //Calculate where particle system should go based on rotation
        Vector2 Vdirection = MathFunctions.DegreesToVector(direction);
        Vector2 offset = Vdirection * (lightningParticles.shape.scale.x / 2);
        
        ParticleSystem system = Instantiate(lightningParticles);
        system.transform.position = transform.position;
        system.transform.rotation = Quaternion.Euler(direction - 180, -90, -90);
        var main = system.main;
        main.startRotation = -direction * Mathf.Deg2Rad;
        
        //Trigger an AOE damaging all enemies inside
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position + (Vector3)offset, new Vector2(20, 1), direction,targetLayers);
        foreach (var t in targets)
        {
            Health h = t.GetComponent<Health>();
            if (h)
            {
                h.Damage(Damage, DamageType, new Vector2(0, 0), gameObject);
            }
        }
    }

    void Start()
    {
        CharacterAttack = new GarfAttack(gameObject.GetComponent<AlliedAI>());
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        /*
        if (InputSystem.actions.FindAction("Attack").WasPressedThisFrame())
        {
            Attack();
        }
        */
    }
}
