using System.Threading.Tasks;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class EnemyStun : AIState
{
    public EnemyStun(EnemyAI ai)
    {
        EAi = ai;
    }

    public override void Enter()
    {
        //start the stun
        EAi.moveTo.SetSpeed(0);
        
        EndStun();
    }

    public override void Update()
    {
        //nothing happens in stun
    }

    public override void Exit()
    {
        //end the stun
        EAi.moveTo.SetSpeed(EAi.movementSpeed);
    }

    public async void EndStun()
    {
         await Task.Delay((int)(EAi.stunDuration * 1000f));
         EAi.RequestState(States.EnemySearching);
    }
}