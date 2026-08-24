using System;
using UnityEngine;

public class Attack : AIState
{
    public Attack(AlliedAI ai)
    {
        Ai = ai;
    }
    public override void Enter()
    {
        Debug.Log("Begin Attack");
    }

    public override void Update()
    {
        Debug.Log("attacking");
        GameObject nearestEnemy = GameObjectLocator.FindNearestWithTag(Ai.player.gameObject, "Enemy");
        if (Vector3.Distance(Ai.player.transform.position, nearestEnemy.transform.position) > Ai.aggressionDistance)
        {
            Ai.RequestState(new Defend(Ai));
        }
        else
        {
            Ai.ally.Attack();
        }
    }

    public override void Exit()
    {
        Debug.Log("End Attack");
    }
}
