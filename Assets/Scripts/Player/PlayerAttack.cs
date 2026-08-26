using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public DamageTypes damageType;
    public float attackSpeed;
    public float knockbackStrength;
    public LayerMask targetLayers;
    public ParticleSystem particles;

    [NonSerialized]
    public float _attackTimer;
    private SpriteRenderer _rn;

    private InputAction _attackAction;

    void Start()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");
        
        _rn = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_attackAction.WasPressedThisFrame() && _attackTimer > attackSpeed)
        {
            //Damage Targets
            RaycastHit2D[] targets;
            Vector2 boxSize = new Vector2(2, 2);
            targets = _rn.flipX ? Physics2D.BoxCastAll(transform.position + new Vector3(-1, 0, 0), boxSize, 0, Vector2.zero, 0, targetLayers) : Physics2D.BoxCastAll(transform.position + new Vector3(1, 0, 0), boxSize, 0, Vector2.zero, 0, targetLayers);
            foreach (RaycastHit2D hit in targets)
            {
                Vector2 knockback = -(transform.position - hit.transform.position).normalized * knockbackStrength;
                if (hit.collider.gameObject.GetComponent<Health>())
                {
                    hit.collider.gameObject.GetComponent<Health>().Damage(damage, damageType, knockback);
                }
            }
            
            //Play Animation
            ParticleSystem system = Instantiate(particles, gameObject.transform);
            system.transform.position = _rn.flipX ? transform.position + new Vector3(-1, 0, 0) : transform.position + new Vector3(1, 0, 0);

            if (_rn.flipX)
                system.transform.localScale = new Vector3(-1, system.transform.localScale.y, system.transform.localScale.z);
            
            system.Play();
            
            //Reset Attack Timer
            _attackTimer = 0;
        }
        _attackTimer += Time.deltaTime;
    }
}
