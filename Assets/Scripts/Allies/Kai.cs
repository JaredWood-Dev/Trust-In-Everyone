using UnityEngine;
using UnityEngine.InputSystem;

public class Kai : MonoBehaviour, IAlly
{
    public int Damage { get; set; }
    public DamageTypes DamageType { get; set; }
    
    public float AttackSpeed { get; set; }
    
    //Direction to aim the fire
    [Header("Targeting")]
    public float direction;
    public LayerMask targetLayers;
    public ParticleSystem fireParticles;
    
    public Attack CharacterAttack { get; set; }

    //Kai's attack is a flamethrower / burning hands
    
    public void Attack()
    {
        //Calculate rotation based on enemies
        GameObject target = GameObjectLocator.FindNearestWithTag(gameObject, "Enemy");
        Vector2 diff = (target.transform.position - transform.position).normalized;
        direction = MathFunctions.VectorToDegrees(diff);
        
        Vector2 Vdirection = MathFunctions.DegreesToVector(direction);
        Vector2 offset = Vdirection * (1.5f);
        
        ParticleSystem system = Instantiate(fireParticles, transform.position, Quaternion.Euler(new Vector3(0, 0, direction)));
        var main = system.main;
        main.startRotation = direction;
        
        //Trigger an AOE damaging all enemies inside
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position + (Vector3)offset, new Vector2(1, 3), direction,targetLayers);
        foreach (var t in targets)
        {
            Health h = t.GetComponent<Health>();
            if (h)
            {
                h.Damage(Damage, DamageType, new Vector2(0, 0));
            }
        }
    }
    
    void Start()
    {
    }

    void Update()
    {
        
    }
}
