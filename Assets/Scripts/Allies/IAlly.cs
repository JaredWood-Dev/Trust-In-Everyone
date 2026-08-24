using UnityEngine;

public interface IAlly
{
    public float AttackSpeed { get; set; }
    public Attack CharacterAttack { get; set; }
    public void Attack();
}
