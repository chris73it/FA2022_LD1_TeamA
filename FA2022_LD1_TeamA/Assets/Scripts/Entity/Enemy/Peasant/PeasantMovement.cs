using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeasantMovement : EnemyMovement
{
    public enum PeasantStates
    {
        Wander,
        Pursuit,
        Attacking
    }

    public PeasantStates State = PeasantStates.Wander;
    public float WanderRange = 2f;
    public bool Wandering = false;
    public bool Pursuing = false;
    public PeasantCombat Combat;

    public override void Initialize()
    {
        base.Initialize();
        Combat = GetComponent<PeasantCombat>();
    }

    private void Update()
    {
        if (State == PeasantStates.Wander)
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
        if (State == PeasantStates.Pursuit)
        {
            if (!Pursuing)
            {
                Pursuit();
                Wandering = false;
            }
            if (Pursuing)
            {
                Destination = Player.transform.position;
            }

            Move();
        }
        if (State != PeasantStates.Pursuit)
        {
            Pursuing = false;
        }
        if (State == PeasantStates.Attacking)
        {
            TopSpeed = 0f;
        }
    }

    public void Wander()
    {
        TopSpeed = 1f;
        Vector3 randomPosition = Random.insideUnitSphere * WanderRange;
        randomPosition += transform.position;
        Wandering = true;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, WanderRange, 1);
        StateTimer = 3f;
        Destination = hit.position;
        //Debug.Log(transform.position);
    }

    public void Pursuit()
    {
        TopSpeed = 6f;
        Pursuing = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (State == PeasantStates.Wander)
            {
                Wander(); // maybe there should be a better failsafe for this or change NavMeshAgent.remainingDistance <= 0 to something else
            }
        }
    }
}
