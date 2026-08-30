using UnityEngine;

public class WinterAttack : Attack
{
    public WinterAttack(AlliedAI ai)
    {
        Ai = ai;
    }

    public override void Update()
    {
        //Stay Near Coal
        Ai.moveTo.SetDestination(Ai.player);
        
        GameObject enemy = Ai.target;
        if (enemy)
        {
            if (Vector3.Distance(Ai.gameObject.transform.position, enemy.transform.position) < Ai.attackDistance)
            {
                if (Ai.AttackTimer > Ai.ally.AttackSpeed)
                {
                    Ai.ally.Attack();
                    Ai.AttackTimer = 0;
                }
            }
            else
            {
                Ai.RequestState(States.Defending);
            }
        }
        else
        {
            Ai.RequestState(States.Defending);
        }
    }
}
