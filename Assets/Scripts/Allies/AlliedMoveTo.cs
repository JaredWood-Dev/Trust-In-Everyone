using UnityEngine;

public class AlliedMoveTo : MoveTo
{
    [Header("Ally Offset")]
    public Vector2 allyOffset;

    public override void SetDestination(GameObject obj)
    {
        _ai.destination = obj.transform.position + (Vector3)allyOffset;
    }
}
