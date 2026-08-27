using System;
using System.Reflection.Metadata.Ecma335;
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
    public ParticleSystem rallyFlag;

    [NonSerialized]
    public float _attackTimer;
    private SpriteRenderer _rn;

    private InputAction _attackAction;
    private InputAction _attackOrder;
    private InputAction _defendOrder;

    void Start()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");
        _attackOrder = InputSystem.actions.FindAction("Command Attack");
        _defendOrder = InputSystem.actions.FindAction("Command Defend");
        
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
                    hit.collider.gameObject.GetComponent<Health>().Damage(damage, damageType, knockback, gameObject);
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

        if (_defendOrder.WasPressedThisFrame())
        {
            EventManager.DefendOrder();
        }

        if (_attackOrder.WasPressedThisFrame())
        {
            Vector2 mousePos = Mouse.current.position.ReadValue(); 
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            ParticleSystem flag = Instantiate(rallyFlag, mousePos, Quaternion.identity);
            var objectHit = Physics2D.Raycast(mousePos, Vector2.zero, 10, targetLayers);
            if (objectHit.collider)
            {
                EventManager.AttackOrder(objectHit.collider.gameObject);
            }
            else
            {
                EventManager.PointOrder(mousePos);
            }
        }
        
        _attackTimer += Time.deltaTime;
    }
}
