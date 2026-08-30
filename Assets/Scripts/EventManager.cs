using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject, GameObject, int, DamageTypes> OnCreatureHit;
    public static event Action<GameObject, GameObject> OnEnemyDeath;
    public static event Action OnWaveEnd;
    public static event Action OnDefendOrder;
    public static event Action<GameObject> OnAttackOrder;
    public static event Action<Vector2> OnPointOrder;
    public static event Action OnBossDied;

    public static void CreatureHit(GameObject target, GameObject attacker, int damage, DamageTypes damageType = DamageTypes.Physical)
    {
        OnCreatureHit?.Invoke(target, attacker, damage, damageType);
    }

    public static void EnemyKilled(GameObject target, GameObject killer)
    {
        OnEnemyDeath?.Invoke(target, killer);    
    }

    public static void WaveEnd()
    {
        OnWaveEnd?.Invoke();
    }

    public static void DefendOrder()
    {
        OnDefendOrder?.Invoke();
    }

    public static void AttackOrder(GameObject target)
    {
        OnAttackOrder?.Invoke(target);
    }

    public static void PointOrder(Vector2 location)
    {
        OnPointOrder?.Invoke(location);
    }

    public static void BossDied()
    {
        OnBossDied?.Invoke();
    }
}
