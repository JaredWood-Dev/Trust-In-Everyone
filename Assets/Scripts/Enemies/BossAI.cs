using System.Collections;
using Pathfinding;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    //boss has three moves:
    //- throw boulders
    //- giant stomp
    //- pause
    
    [Header("Throwing Boudlers")]
    public GameObject boulder;
    public int boulderDamage;
    public float boulderSpeed;
    [SerializeField]
    public Vector2[] throwLocations;

    [Header("Stomp")] 
    public float stompSize;
    public float stompDelay;
    public int stompDamage;
    public ParticleSystem stompParticles;
    public LayerMask targetLayers;
    public AudioClip stompSound;
    
    private MoveTo _moveTo;
    private AIPath _aiPath;
    private GameObject _player;
    private Animator _animator;
    private AudioSource _audioSource;


    void Start()
    {
        _moveTo = GetComponent<MoveTo>();
        _aiPath = GetComponent<AIPath>();
        _player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        StartCoroutine(BossLoop());
    }

    public IEnumerator BossLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ThrowBoulders());
            yield return StartCoroutine(GiantStomp());
        }
    }
    
    public IEnumerator ThrowBoulders()
    {
        for (int i = 0; i < throwLocations.Length; i++)
        {
            _moveTo.SetDestination(throwLocations[i]);
            _aiPath.destination = throwLocations[i];

            while (!_aiPath.reachedDestination)
            {
                yield return null;
            }
            
            GameObject b = Instantiate(boulder, gameObject.transform);
            b.transform.position = transform.position + new Vector3(-1, 3, 0);
            yield return StartCoroutine(DelayThrow(b, 2f));
        }
    }

    public IEnumerator GiantStomp()
    {
        _aiPath.destination = _player.transform.position;
        
        while (!_aiPath.reachedDestination)
        {
            yield return null;
        }

        yield return StartCoroutine(DelayStomp(stompDelay));
    }

    public void Wander()
    {
        
    }

    IEnumerator DelayThrow(GameObject boulderCopy, float delay)
    {
        yield return new WaitForSeconds(delay);
      
        Vector2 diffVector = boulderCopy.transform.position - GameObjectLocator.FindNearestWithTag(gameObject, "Ally").transform.position;
        Vector2 boulderVelocity = -diffVector.normalized * boulderSpeed;
        boulderCopy.transform.parent = null;
        boulderCopy.GetComponent<BossProjectile>().damage = boulderDamage;
        boulderCopy.GetComponent<BossProjectile>().isActive = true;
        boulderCopy.GetComponent<Rigidbody2D>().linearVelocity = boulderVelocity;
    }

    IEnumerator DelayStomp(float delay)
    {
        _animator.SetTrigger("triggerStomp");
        
        yield return new WaitForSeconds(delay);
        
        _animator.SetTrigger("triggerStompEnd");
        
        _audioSource.clip = stompSound;
        _audioSource.Play();

        if (stompParticles)
        {
            ParticleSystem pSystem = Instantiate(stompParticles, transform.position, Quaternion.identity);
            var s = pSystem.shape;
            s.radius = stompSize;
            var e = pSystem.emission;
            var burst = e.GetBurst(0);
            burst.count = 50;
            e.SetBurst(0, burst);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stompSize, targetLayers);
        foreach (var target in colliders)
        {
            Health health = target.GetComponent<Health>();
            if (health)
            {
                health.Damage(stompDamage, DamageTypes.Physical, Vector2.one, gameObject);
            }
        }
    }
}
