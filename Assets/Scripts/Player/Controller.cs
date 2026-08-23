using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float movementSpeed;
    
    private InputAction _moveAction;
    private Vector2 _movement;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        _movement = _moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (_movement.x != 0 || _movement.y != 0)
        {
            transform.Translate(_movement * (movementSpeed * Time.deltaTime));
        }
    }
}
