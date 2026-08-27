using Pathfinding;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    protected AIPath _ai;
    private SpriteRenderer _rn;
    private Animator _an;

    void Start()
    {
        _ai = GetComponent<AIPath>();
        _rn = GetComponent<SpriteRenderer>();
        _an = GetComponent<Animator>();
    }

    void Update()
    {
        if (_ai.destination.x < transform.position.x)
        {
            _rn.flipX = true;
        }

        if (_ai.destination.x > transform.position.x)
        {
            _rn.flipX = false;
        }

        if (_ai.reachedDestination)
        {
            _an.SetBool("isWalking", false);    
        }
        else
        {
            _an.SetBool("isWalking", true);
        }
    }

    public void SetDestination(Vector2 destination)
    {
        _ai.destination = destination;
    }

    public virtual void SetDestination(GameObject obj)
    {
        _ai.destination = obj.transform.position;
    }

    public void SetSpeed(float speed, float acceleration = 0)
    {
        _ai = GetComponent<AIPath>();
        if (_ai)
            _ai.maxSpeed = speed;
        
        if (acceleration == 0)
            if (_ai)
                _ai.maxAcceleration = speed * 0.9f;
    }
}
