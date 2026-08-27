using System;
using UnityEngine;

public class Attack : AIState
{
    public Attack(AlliedAI ai)
    {
        Ai = ai;
    }

    public Attack()
    {
        
    }
    public override void Enter()
    {
        
    }

    public override void Update()
    {
        if (Vector3.Distance(Ai.gameObject.transform.position, Ai.player.transform.position) > Ai.returnDistance)
        {
            Ai.RequestState(States.Defending);
        }
        GameObject nearestEnemy = GameObjectLocator.FindNearestWithTag(Ai.gameObject.gameObject, "Enemy");
        if (!nearestEnemy)
        {
            Ai.RequestState(States.Defending);
        }
        else {
            if (Vector3.Distance(Ai.gameObject.transform.position, nearestEnemy.transform.position) > Ai.aggressionDistance)
            {
                Ai.RequestState(States.Defending);
            }
            else
            {
                Vector2 differenceVector = Ai.player.transform.position - nearestEnemy.transform.position;
                Vector2 offset = differenceVector.normalized * 2f;
                Vector2 targetLocation = (Vector2)nearestEnemy.transform.position + offset;

                Ai.moveTo.SetDestination(targetLocation);
                if (Vector3.Distance(Ai.gameObject.transform.position, nearestEnemy.transform.position) <
                    Ai.attackDistance && Ai.AttackTimer > Ai.ally.AttackSpeed)
                {
                    Ai.ally.Attack();
                    Ai.AttackTimer = 0;
                }
            }
        }
    }

    public override void Exit()
    {
        
    }
}
