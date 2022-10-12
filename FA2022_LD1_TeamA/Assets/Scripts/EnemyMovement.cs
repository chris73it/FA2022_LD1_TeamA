using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement
{
    public NavMeshAgent NavMeshAgent;
    public GameObject Player;
    public Vector3 Destination;
    public enum EnemyMovementTypes
    {
        Idle,
        Wander,
        Chase,
        /*
        Attack
        */
    }
    public EnemyMovementTypes Type = EnemyMovementTypes.Idle;
    public float StateTimer = 0f;
    public float WanderRange = 1f;
    public float ChaseRange = 3f;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = TopSpeed;
        NavMeshAgent.acceleration = Acceleration;
        Player = GameManager.ChosenPlayerCharacter;
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (StateTimer <= 0f) // rethink statetimer usage
        {
            ChooseNewState();
            switch (Type)
            {
                case (EnemyMovementTypes.Idle):
                    Idle();
                    break;

                case (EnemyMovementTypes.Wander):
                    Wander();
                    break;

                case (EnemyMovementTypes.Chase):
                    Chase();
                    break;
            }
        } else
        {
            if (NavMeshAgent.remainingDistance <= 0)
            {
                StateTimer = 0f;
            } else if (IsPlayerInRange(ChaseRange))
            {
                StateTimer = 0f;
                Type = EnemyMovementTypes.Chase;
            } else
            {
                StateTimer -= Time.deltaTime;
            }
        }
        */

        if (IsPlayerInRange(ChaseRange))
        {
            Chase();
        }
        else
        {
            if (Random.Range(0,2) == 0)
            {
                Idle();
            } else
            {
                Wander();
            }
        }
        
        //Debug.Log("Type: " + Type);

        Move();
    }

    public void Idle()
    {
        Type = EnemyMovementTypes.Idle;
        Destination = transform.position;
    }

    public void Wander()
    {
        Type = EnemyMovementTypes.Wander;
        Vector3 randomPosition = Random.insideUnitSphere * WanderRange;
        randomPosition += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, WanderRange, 1);
        Destination = hit.position;
        Debug.Log(transform.position);
    }

    public override void Move()
    {
        //Debug.Log("Move");
        NavMeshAgent.destination = Destination;
    }


    public void ChooseNewState()
    {
        /*
        int statesLength = EnemyMovementTypes.GetNames(typeof(EnemyMovementTypes)).Length;
        EnemyMovementTypes newState = Type;

        while (newState == Type || (Type == EnemyMovementTypes.Chase && IsPlayerInRange(ChaseRange)))
        {
            newState = (EnemyMovementTypes)Random.Range(0, statesLength);
        }

        Type = newState;
        */
    }

    public void Chase()
    {
        Type = EnemyMovementTypes.Chase;
        Destination = Player.transform.position;
    }

    public bool IsPlayerInRange(float range)
    {
        if (Player != null)
        {
            return Vector3.Distance(transform.position, Player.transform.position) <= range;
        }

        return false;
    } 
}
