using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject, GameObject, int, DamageTypes> OnCreatureHit;

    public static void CreatureHit(GameObject target, GameObject attacker, int damage, DamageTypes damageType = DamageTypes.Physical)
    {
        OnCreatureHit?.Invoke(target, attacker, damage, damageType);
    }
}
