using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float movementSpeed;
    
    private InputAction _moveAction;
    private Vector2 _movement;

    private Animator _an;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        
        _an = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _movement = _moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 movement = ((_movement * movementSpeed) - _rb.linearVelocity) / Time.deltaTime;
        Vector2 force = movement * _rb.mass;
        _rb.AddForce(force);
        
        if (_movement.x != 0 || _movement.y != 0)
        {
            _an.SetBool("isWalking", true);
        }
        else
        {
            _an.SetBool("isWalking", false);
        }

        if (_rb.linearVelocityX < 0)
        {
            _sr.flipX = true;
        }
        if (_rb.linearVelocityX > 0)
        {
            _sr.flipX = false;
        }
    }
}
