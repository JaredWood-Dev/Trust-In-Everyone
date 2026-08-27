using System;
using UnityEngine;

public class Defend : AIState
{
    public Defend(AlliedAI ai)
    {
        Ai = ai;
    }
    public override void Enter()
    {
       
    }

    public override void Update()
    {
        Ai.moveTo.SetDestination(Ai.player);
        GameObject nearestEnemy = GameObjectLocator.FindNearestWithTag(Ai.player.gameObject, "Enemy");
        if (nearestEnemy)
        {
            if (Vector3.Distance(Ai.gameObject.transform.position, nearestEnemy.transform.position) < Ai.aggressionDistance)
            {
                Ai.target = nearestEnemy;
                Ai.RequestState(States.Attacking);
            }
        }
    }

    public override void Exit()
    {
        
    }
}
