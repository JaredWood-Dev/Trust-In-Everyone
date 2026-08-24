using Pathfinding;
using UnityEngine;

public class DefendAction : MonoBehaviour
{
    //The target to defend
    public GameObject target;
    
    //The offset of the target's position to move to
    public Vector2 offset;

    private AIPath _ai;

    void Start()
    {
        _ai = GetComponent<AIPath>();
    }

    void Update()
    {
        _ai.destination = target.transform.position + (Vector3)offset;
    }
}
