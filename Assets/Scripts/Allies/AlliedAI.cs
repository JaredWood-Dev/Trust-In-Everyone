using System;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class AlliedAI : MonoBehaviour
{
    //State machine that controls the allied AI
    public float aggressionDistance;

    public GameObject player;
    public IAlly ally;
    
    [Serialize]
    public AIState currentState;

    void Start()
    {
        //Default state is defend
        currentState = new Defend(this);
        
        player = GameObject.FindGameObjectWithTag("Player");
        ally = GetComponent<IAlly>();
    }

    void Update()
    {
        print("Current State: " + currentState);
        //Trigger the state's update
        currentState.Update();
    }

    //Changes the state to the requested state
    public void RequestState(AIState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
