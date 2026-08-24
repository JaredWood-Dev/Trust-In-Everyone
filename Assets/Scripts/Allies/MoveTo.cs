using Pathfinding;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    //The target to defend
    public GameObject target;
    
    //The offset of the target's position to move to
    public Vector2 offset;

    private AIPath _ai;
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
        _ai.destination = target.transform.position + (Vector3)offset;
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
}
