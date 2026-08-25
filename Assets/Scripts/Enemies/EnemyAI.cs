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

        //default state
        _currentState = new EnemySearch(this);
        if (isCultist) _currentState = new CultistSearch(this);
    }

    private void Update()
    {
        print(_currentState);
        _currentState.Update();
        
        AttackTimer += Time.deltaTime;
    }

    public void RequestState(States state)
    {
        print(state);
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
                _currentState = new CultistSearch(this);
                break;
        }
        _currentState.Enter();
    }
}
