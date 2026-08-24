using Pathfinding;
using UnityEngine;

public class DefendAction : MonoBehaviour
{
    //The target to defend
    public GameObject target;
    
    //The offset of the target's position to move to
    public Vector2 offset;

    private AIPath _ai;
    private SpriteRenderer _rn;

    void Start()
    {
        _ai = GetComponent<AIPath>();
        _rn = GetComponent<SpriteRenderer>();
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
    }
}
