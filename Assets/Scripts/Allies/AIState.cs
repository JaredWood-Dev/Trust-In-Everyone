using UnityEngine;

public abstract class AIState
{
    //Abstract class that represents different states the allied AI can be in
    //TODO: replace with inheritence based system
    protected AlliedAI Ai;
    protected EnemyAI EAi;

    protected AIState(AlliedAI ai)
    {
        Ai = ai;
    }

    protected AIState(EnemyAI ai)
    {
        EAi = ai;
    }

    protected AIState()
    {
        Ai = null;
    }

    //Occurs when first entering a state
    public virtual void Enter()
    {
        
    }

    //Occurs every frame
    public virtual void Update()
    {
        
    }

    //Occurs when leaving a state
    public virtual void Exit()
    {
        
    }
}
