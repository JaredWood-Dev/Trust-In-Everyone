using UnityEngine;

public class EnemySearch : AIState
{
    //In enemy search, the enemies slowly wanted toward Coal

    public EnemySearch(EnemyAI ai)
    {
        EAi = ai;
    }
    
    public override void Update()
    {
        //Move to Coal
        if (EAi.player)
            EAi.moveTo.SetDestination(EAi.player.transform.position);
        
        GameObject closestAlly = GameObjectLocator.FindNearestWithTag(EAi.gameObject, "Ally");
        if (closestAlly)
        {
            if (Vector3.Distance(EAi.gameObject.transform.position, closestAlly.transform.position) <
                EAi.aggressionDistance)
            {
                EAi.target = closestAlly;
                EAi.RequestState(States.EnemyFollowing);
            }
        }
    }
}
