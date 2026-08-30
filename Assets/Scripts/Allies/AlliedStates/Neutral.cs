using Pathfinding;
using UnityEngine;

public class Neutral : AIState
{
    public Neutral(AlliedAI ai)
    {
        Ai = ai;
    }

    public override void Update()
    {
        Ai.moveTo.SetDestination(Ai.destination);
        
        //once close, return to normal activity
        if (Vector3.Distance(Ai.gameObject.transform.position, Ai.destination) <= 2f)
        {
            Ai.RequestState(States.Defending);
        }
    }
}