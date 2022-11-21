using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackMovement : EnemyMovement
{
    public enum LumberjackStates
    {
        Attacking,
        Prepping,
        Jumping,
        Slamming
    }
    private LumberjackStates state = LumberjackStates.Attacking;
    public LumberjackStates State
    {
        get { return state; } 
        set
        {
            state = value;
            switch (state)
            {
                case (LumberjackStates.Attacking):
                    Combat.AttackCooldown = 5f;
                    break;

                case (LumberjackStates.Prepping):
                    GetChargeLocation();
                    State = LumberjackStates.Jumping;
                    break;

                case (LumberjackStates.Jumping):
                    Debug.Log("Jumping...");
                    break;

                case (LumberjackStates.Slamming):
                    Debug.Log("Slamming...");
                    
                    break;

                default:
                    break;
            }
        }
    }
    public LumberjackCombat Combat;

    public override void Initialize()
    {
        base.Initialize();
        StateTimer = Random.Range(10, 15);
        TopSpeed = 8f;
        Combat = GetComponent<LumberjackCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Combat.IsStunned <= 0f)
        {
           if (State == LumberjackStates.Jumping) {

                Move();

                if (NavMeshAgent.remainingDistance <= 0)
                {
                    State = LumberjackStates.Slamming;
                } 
           } else
           {
                if (StateTimer <= 0)
                {
                    State = LumberjackStates.Prepping;
                }
           }
        }

        if (State == LumberjackStates.Slamming)
        {
            //Combat.SlamAttack(); 
            StateTimer = Random.Range(20, 25);
            State = LumberjackStates.Attacking;
        }

        if (StateTimer > 0)
        {
            StateTimer -= Time.deltaTime;
        }
    }

    public void GetChargeLocation()
    {
        Destination = GameManager.ChosenPlayerCharacter.transform.position; // if rushes into wall then it becomes glitch on the wall
    }
}
