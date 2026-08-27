using System;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class AlliedAI : MonoBehaviour
{
    //State machine that controls the allied AI
    public float aggressionDistance;
    public float attackDistance;
    public float returnDistance = 15; //distance that allies return to coal

    public GameObject player;
    public GameObject target;
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
