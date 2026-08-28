using UnityEngine;

public class MagmancerAttack : EnemyAttack
{
    private MagmancerAI MAi;
    public MagmancerAttack(MagmancerAI eai)
    {
        MAi = eai;
    }

    public override void Update()
    {
        if (MAi.target)
        {
            Vector2 diffVector = MAi.transform.position - MAi.target.transform.position;
            float offsetDistance = 10f;
            Vector2 offesetVector = diffVector.normalized * offsetDistance;

            MAi.moveTo.SetDestination(MAi.target.transform.position + (Vector3)offesetVector);

            if (Vector2.Distance(MAi.transform.position, MAi.target.transform.position) < MAi.attackDistance)
            {
                if (MAi.AttackTimer > MAi.attackSpeed)
                {
                    MAi.AttackTimer = 0;
                    EnemyProjectile projectile = MAi.SummonProjectile().GetComponent<EnemyProjectile>();
                    projectile.gameObject.transform.position = MAi.gameObject.transform.position;
                    projectile.damageType = MAi.damageType;
                    projectile.attacker = MAi.gameObject;
                    projectile.damage = MAi.attackDamage;
                    projectile.velocity = -diffVector.normalized * 25f;
                }
            }
        }
        else
        {
            MAi.RequestState(States.EnemySearching);
        }
    }
}