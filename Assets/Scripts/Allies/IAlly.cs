using UnityEngine;

public interface IAlly
{
    public int Damage { get; set; }
    public DamageTypes DamageType { get; set; }
    
    public float AttackSpeed { get; set; }
    public Attack CharacterAttack { get; set; }
    public void Attack();
}
