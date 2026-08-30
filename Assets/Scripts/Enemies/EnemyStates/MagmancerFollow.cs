using UnityEngine;

public class MagmancerFollow : EnemyFollow
{
    public MagmancerFollow(EnemyAI ai)
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
        if (EAi.target)
        {
            Vector2 diffVector = EAi.target.transform.position - EAi.target.transform.position;
            float offsetDistance = 10f;
            Vector2 offesetVector = diffVector.normalized * offsetDistance;
        
            EAi.moveTo.SetDestination(EAi.target.transform.position + (Vector3)offesetVector);

            if (Vector3.Distance(EAi.gameObject.transform.position, EAi.target.transform.position) < EAi.attackDistance)
            {
                EAi.RequestState(States.EnemyAttacking);
            }
            
            GameObject closestAlly = GameObjectLocator.FindNearestWithTag(EAi.gameObject, "Ally");
            if (Vector3.Distance(EAi.transform.position, closestAlly.transform.position) > EAi.aggressionDistance)
            {
                EAi.RequestState(States.EnemySearching);
            }
        }
    }
}