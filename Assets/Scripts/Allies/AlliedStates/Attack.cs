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
        GameObject nearestEnemy = GameObjectLocator.FindNearestWithTag(Ai.player.gameObject, "Enemy");
        if (!nearestEnemy)
        {
            Ai.RequestState(new Defend(Ai));
        }
        else {
            if (Vector3.Distance(Ai.player.transform.position, nearestEnemy.transform.position) > Ai.aggressionDistance)
            {
                Ai.RequestState(new Defend(Ai));
            }
            else
            {
                Ai.moveTo.SetDestination(nearestEnemy.transform.position);
                if (Vector3.Distance(Ai.gameObject.transform.position, nearestEnemy.transform.position) <
                    Ai.aggressionDistance && Ai.AttackTimer > Ai.ally.AttackSpeed)
                {
                    Ai.ally.Attack();
                    Ai.AttackTimer = 0;
                }
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("End Attack");
    }
}
