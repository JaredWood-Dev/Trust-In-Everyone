using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Garf : MonoBehaviour, IAlly
{

    public int damage;
    public DamageTypes damageType;
    public float attackSpeed;
    
    //Direction to aim the bolt
    [Header("Targeting")]
    public float direction;

    public LayerMask targetLayers;
    
    public ParticleSystem lightningParticles;
    
    //Garf's attack is a bolt of lightning
    
    public void Attack()
    {
        //Calculate rotation based on enemies
        GameObject target = GameObjectLocator.FindNearestWithTag(gameObject, "Enemy");
        Vector2 diff = (target.transform.position - transform.position).normalized;
        direction = MathFunctions.VectorToDegrees(diff);
        
        //Calculate where particle system should go based on rotation
        Vector2 Vdirection = MathFunctions.DegreesToVector(direction);
        Vector2 offset = Vdirection * (lightningParticles.shape.scale.x / 2);
        
        ParticleSystem system = Instantiate(lightningParticles, transform.position + (Vector3)offset, Quaternion.Euler(new Vector3(0, 0, direction)));
        var main = system.main;
        main.startRotation = direction;
        
        //Trigger an AOE damaging all enemies inside
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position + (Vector3)offset, new Vector2(lightningParticles.shape.scale.x, 1), direction,targetLayers);
        foreach (var t in targets)
        {
            Health h = t.GetComponent<Health>();
            if (h)
            {
                h.Damage(damage, damageType, new Vector2(0, 0));
            }
        }
    }

    void Update()
    {
        
    }
}
