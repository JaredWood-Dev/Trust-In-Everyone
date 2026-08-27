using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float aggressionDistance = 10;
    public float attackDistance = 1;
    public float movementSpeed = 1;
    public int attackDamage = 5;
    public float attackSpeed = 1;
    public float knockback = 10;
    public DamageTypes damageType;
    public float stunDuration = 1;
    
    public bool isCultist = false; //TODO: CHANGE LATER

    public GameObject player;
    public GameObject target;
    public MoveTo moveTo;

    private AIState _currentState;
    [NonSerialized]
    public float AttackTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveTo = GetComponent<MoveTo>();
        moveTo.SetSpeed(movementSpeed);

        //default state
        _currentState = new EnemySearch(this);
        if (isCultist) _currentState = new CultistSearch(this);
    }

    private void Update()
    {
        _currentState.Update();
        
        AttackTimer += Time.deltaTime;
        print(_currentState);
        print(target);
    }

    public void RequestState(States state)
    {
        _currentState.Exit();
        switch (state)
        {
            case States.EnemyAttacking:
                _currentState = new EnemyAttack(this);
                break;
            case States.EnemyFollowing:
                _currentState = new EnemyFollow(this);
                break;
            case States.EnemySearching:
                _currentState = new EnemySearch(this);
                if (isCultist)
                    _currentState = new CultistSearch(this);
                break;
            case States.EnemyStunned:
                _currentState = new EnemyStun(this);
                break;
        }
        _currentState.Enter();
    }

    void EnemyHit(GameObject victim, GameObject attacker, int damage, DamageTypes damageType)
    {
        
        if (victim == gameObject)
        {
            print("setting target to: " + attacker.name);
            RequestState(States.EnemyStunned);
            target = attacker;
        }
    }

    void OnEnable()
    {
        EventManager.OnCreatureHit += EnemyHit;
    }

    private void OnDisable()
    {
        EventManager.OnCreatureHit -= EnemyHit;
    }
}
