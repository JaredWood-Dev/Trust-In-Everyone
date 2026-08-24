using System;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class AlliedAI : MonoBehaviour
{
    //State machine that controls the allied AI
    public float aggressionDistance;
    public float attackDistance;

    public GameObject player;
    public IAlly ally;
    public MoveTo moveTo;
    
    
    private AIState currentState;
    [NonSerialized]
    public float AttackTimer;

    void Start()
    {
        //Default state is defend
        currentState = new Defend(this);
        
        player = GameObject.FindGameObjectWithTag("Player");
        ally = GetComponent<IAlly>();
        moveTo = GetComponent<MoveTo>();
    }

    void Update()
    {
        print("Current State: " + currentState);
        //Trigger the state's update
        currentState.Update();
        
        AttackTimer += Time.deltaTime;
    }

    //Changes the state to the requested state
    public void RequestState(States newState)
    {
        currentState.Exit();
        switch (newState)
        {
            case States.Defending:
                currentState = new Defend(this);
                break;
            case States.Attacking:
                if (ally.CharacterAttack != null)
                {
                    currentState = ally.CharacterAttack;
                }
                else
                {
                    currentState = new Attack(this);
                }
                break;
        }
        currentState.Enter();
    }
}
