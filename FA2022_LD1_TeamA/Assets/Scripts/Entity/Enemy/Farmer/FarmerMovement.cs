using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerMovement : EnemyMovement
{
    public enum FarmerStates
    {
        Wander,
        Attacking
    }

    public FarmerStates State = FarmerStates.Wander;
    public float WanderRange = 2f;
    public bool Wandering = false;
    public FarmerCombat Combat;

    public override void Intialize()
    {
        base.Intialize();
        Combat = GetComponent<FarmerCombat>();
    }

    private void Update()
    {
        if (Combat.IsStunned >= 0f)
        {
            if (State == FarmerStates.Wander)
            {
                if (!Wandering)
                {
                    Wander();

                }

                if (NavMeshAgent.remainingDistance <= 0)
                {
                    Wandering = false;
                }

                Move();
            }
        }
    }

    public void Wander()
    {
        Vector3 randomPosition = Random.insideUnitSphere * WanderRange;
        randomPosition += transform.position;
        Wandering = true;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, WanderRange, 1);
        StateTimer = 3f;
        Destination = hit.position;
        //Debug.Log(transform.position);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (State == FarmerStates.Wander)
            {
                Wander(); // maybe there should be a better failsafe for this or change NavMeshAgent.remainingDistance <= 0 to something else
            }
        }
    }
}
