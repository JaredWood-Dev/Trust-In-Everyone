using UnityEngine;

public class EnemyAttack : AIState
{
    public EnemyAttack(EnemyAI ai)
    {
        EAi = ai;
    }

    public override void Update()
    {
        if (EAi.target)
        {
            if (EAi.AttackTimer > EAi.attackSpeed)
            {
                EAi.AttackTimer = 0;
                Vector2 knockback = (EAi.target.transform.position - EAi.gameObject.transform.position).normalized * EAi.knockback;
                if (EAi.target.GetComponent<Health>())
                    EAi.target.GetComponent<Health>().Damage(EAi.attackDamage, EAi.damageType, knockback, EAi.gameObject);
            }
        }
        
        EAi.RequestState(States.EnemyFollowing);
    }
}
