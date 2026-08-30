using UnityEngine;

public class CultistSearch : EnemySearch
{
    //Cultist search is same as enemy search, but the cultists will always target Coal

    public CultistSearch(EnemyAI ai)
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
                EAi.target = EAi.player;
                EAi.RequestState(States.EnemyFollowing);
            }
        }
    }
}
