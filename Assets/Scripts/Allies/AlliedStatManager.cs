using UnityEngine;

public class AlliedStatManager : MonoBehaviour
{
    public AllyData allyData;
    
    private Health _health;
    private IAlly _ally;
    private AlliedAI _alliedAI;
    private AlliedMoveTo _moveTo;
    private GameManager _gameManager;

    void Start()
    {
        _health = GetComponent<Health>();
        _ally = GetComponent<IAlly>();
        _alliedAI = GetComponent<AlliedAI>();
        _moveTo = GetComponent<AlliedMoveTo>();
        _gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        
        ApplyStats();
    }

    public void ApplyStats()
    {
        //Health & Regen
        _health.maxHealth = allyData.health;
        _health.health = allyData.health;
        _health.regen = allyData.regen;
        
        //Attack (IAlly)
        _ally.Damage = allyData.damage;
        _ally.DamageType = allyData.damageType;
        _ally.AttackSpeed = allyData.attackSpeed;
        
        //Allied AI
        _alliedAI.aggressionDistance = allyData.aggressionDistance;
        _alliedAI.attackDistance = allyData.attackDistance;
        
        //Movement
        if (_moveTo)
            _moveTo.SetSpeed(allyData.moveSpeed);
        
        //Colors and icons
    }
}
