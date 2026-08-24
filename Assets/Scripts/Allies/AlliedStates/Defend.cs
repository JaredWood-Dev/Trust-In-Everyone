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
        Debug.Log("Begin Defend");
    }

    public override void Update()
    {
        Debug.Log("defending");
        Ai.moveTo.SetDestination(Ai.player);
        GameObject nearestEnemy = GameObjectLocator.FindNearestWithTag(Ai.player.gameObject, "Enemy");
        if (nearestEnemy)
        {
            if (Vector3.Distance(Ai.player.transform.position, nearestEnemy.transform.position) < Ai.aggressionDistance)
            {
                Ai.RequestState(States.Attacking);
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("End Defend");
    }
}
