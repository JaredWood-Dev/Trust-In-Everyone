using System.Threading.Tasks;

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
}