using UnityEngine;

public class EnemyFollow : AIState
{
    public EnemyFollow(EnemyAI ai)
    {
        EAi = ai;
    }

    public override void Update()
    {
        //if no target, then go back to searching
        if (!EAi.target)
        {
            EAi.RequestState(States.EnemySearching);
        }
        
        //follow the specific target
        EAi.moveTo.SetDestination(EAi.target.transform.position);

        if (Vector3.Distance(EAi.gameObject.transform.position, EAi.target.transform.position) < EAi.attackDistance)
        {
            EAi.RequestState(States.EnemyAttacking);
        }
    }
}
