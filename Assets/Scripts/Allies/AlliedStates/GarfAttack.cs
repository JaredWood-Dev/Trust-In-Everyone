using UnityEngine;

public class GarfAttack : Attack
{
    public GarfAttack(AlliedAI ai)
    {
        Ai = ai;
    }

    public override void Update()
    {
        // find a line between Coal and enemies
        // move to center point on that line
        GameObject nearestEnemy = GameObjectLocator.FindNearestWithTag(Ai.gameObject, "Enemy");
        if (nearestEnemy)
        {
            Vector2 differenceVector = Ai.player.transform.position - nearestEnemy.transform.position;
            Vector2 midpoint = new Vector2(differenceVector.x / 2, differenceVector.y / 2);
            Vector2 targetLocation = (Vector2)Ai.player.transform.position - midpoint;

            Ai.moveTo.SetDestination(targetLocation);

            if (Vector3.Distance(Ai.gameObject.transform.position, nearestEnemy.transform.position) < Ai.attackDistance)
            {
                if (Ai.AttackTimer > Ai.ally.AttackSpeed)
                {
                    Ai.ally.Attack();
                    Ai.AttackTimer = 0;
                }
            }
        }
        else
        {
            Ai.RequestState(States.Defending);
        }
    }
}