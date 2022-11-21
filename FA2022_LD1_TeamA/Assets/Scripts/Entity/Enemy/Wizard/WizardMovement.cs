using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardMovement : EnemyMovement
{
    public enum WizardStates
    {
        Wander,
        Pursuit,
        Attacking,
        Retreat
    }

    public WizardStates State = WizardStates.Wander;
    public float WanderRange = 2f;
    public bool Wandering = false;
    public bool Pursuing = false;
    public bool Retreating = false;
    public WizardCombat Combat;

    public override void Initialize()
    {
        base.Initialize();
        Combat = GetComponent<WizardCombat>();
    }

    private void Update()
    {
        if (State == WizardStates.Wander)
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
        if (State == WizardStates.Pursuit)
        {
            if (!Pursuing)
            {
                Pursuit();
            }
            if (Pursuing)
            {
                Destination = Player.transform.position;
            }

            Move();
        }
        if (State == WizardStates.Retreat)
        {
            if (!Retreating)
            {
                Retreat();
            }
            if (Retreating)
            {
                Destination = transform.position * 2 - Player.transform.position;
            }

            Move();
        }
        if (State != WizardStates.Wander)
        {
            Wandering = false;
        }
        if (State != WizardStates.Pursuit)
        {
            Pursuing = false;
        }
        if (State != WizardStates.Retreat)
        {
            Retreating = false;
        }
        if (State == WizardStates.Attacking)
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
        TopSpeed = 1f;
        Pursuing = true;
    }

    public void Retreat()
    {
        TopSpeed = 3f;
        Retreating = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (State == WizardStates.Wander)
            {
                Wander(); // maybe there should be a better failsafe for this or change NavMeshAgent.remainingDistance <= 0 to something else
            }
        }
    }
}
