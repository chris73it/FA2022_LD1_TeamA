using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement
{
    public NavMeshAgent NavMeshAgent;
    public static GameObject Player;
    public Vector3 Destination;
    public float StateTimer = 0f;

    private void Awake()
    {
        Initialize();
    }

    public override void Move()
    {
        //Debug.Log("Move");
        NavMeshAgent.destination = Destination;
    }

    public virtual void Initialize()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = TopSpeed;
        NavMeshAgent.acceleration = TopSpeed;
        if (Player == null)
        {
            Player = GameManager.ChosenPlayerCharacter;
        }        
    }

    public void SetNavMeshSpeed(float speed)
    {
        TopSpeed = speed;
        NavMeshAgent.speed = TopSpeed;
        NavMeshAgent.acceleration = TopSpeed;
    }
    /*
    public virtual void TakeAction()
    {
        Enemy AI code...
    }

    private void Update() {
        if (Combat.IsStunned >= 0f) {
            TakeAction()
        } else {
            Destination = hit.position; //? makes them stay in place
        }
    }
    */
}
