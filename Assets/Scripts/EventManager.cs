using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject, GameObject, int, DamageTypes> OnCreatureHit;
    public static event Action<GameObject, GameObject> OnEnemyDeath;
    public static event Action OnWaveEnd;

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
}
