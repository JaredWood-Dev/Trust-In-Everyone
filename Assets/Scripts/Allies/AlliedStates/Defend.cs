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
        GameObject nearestEnemy = GameObjectLocator.FindNearestWithTag(Ai.player.gameObject, "Enemy");
        if (Vector3.Distance(Ai.player.transform.position, nearestEnemy.transform.position) < Ai.aggressionDistance)
        {
            Ai.RequestState(new Attack(Ai));
        }
    }

    public override void Exit()
    {
        Debug.Log("End Defend");
    }
}
