using System;
using UnityEngine;

public class Retreat : AIState
{
    public Retreat(AlliedAI ai)
    {
        Ai = ai;
    }

    public override void Enter()
    {
        Ai.target = null;
    }
    public override void Update()
    {
        //retreat back to Coal
        Ai.moveTo.SetDestination(Ai.player.transform.position);
        
        //once close, return to normal activity
        if (Vector3.Distance(Ai.gameObject.transform.position, Ai.player.transform.position) <= 2f)
        {
            Ai.RequestState(States.Defending);
        }
    }
}
