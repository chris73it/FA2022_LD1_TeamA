using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PeasantCombat : EnemyCombat
{
    public PeasantMovement Movement;
    public float AttackResult = 0f;

    public override void Initialize()
    {
        base.Initialize();
        SightRange = 10f;
        Damage = 1;
        AttackRange = 3f;
        AttackCooldown = 3f;
        Movement = GetComponent<PeasantMovement>();
    }

    public override void Attack()
    {
        AttackDirection = GetEnemyDirection();
        AttackDelay = 0.3f;
        IsAttacking = true;
        Animator.SetTrigger("Attacking");
    }

    private void Update()
    {

        if (IsPlayerInRange(AttackRange))
        {
            if (Movement.State != PeasantMovement.PeasantStates.Attacking)
            {
                Movement.State = PeasantMovement.PeasantStates.Attacking;
                //Debug.Log("Attacking!");
            }

            if (AttackCooldown <= 0 && !IsAttacking)
            {
                Attack();
                Movement.Destination = transform.position;
                //Debug.Log("attack destination");
            }
        }
        else if (IsPlayerInRange(SightRange))
        {
            if (Movement.State != PeasantMovement.PeasantStates.Pursuit)
            {
                Movement.State = PeasantMovement.PeasantStates.Pursuit;
                //Debug.Log("Pursuit!");
            }
        }
        else
        {
            if (Movement.State != PeasantMovement.PeasantStates.Wander && Movement.StateTimer <= 0f)
            {
                Movement.State = PeasantMovement.PeasantStates.Wander;
                //Debug.Log("WanderCombat");
            }
        }

        if (AttackDelay > 0)
        {
            AttackDelay -= Time.deltaTime;
        }
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        if (AttackResult > 0)
        {
            AttackResult -= Time.deltaTime;
        }

        BaseTimers();
    }
    
    void OnDrawGizmos()
    {
        if (IsAttacking)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(AttackDirection + transform.position, new Vector3(1, 1, 1));
        }
        else if (AttackResult > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(AttackDirection + transform.position, new Vector3(1, 1, 1));
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(GetEnemyDirection() + transform.position, new Vector3(1, 1, 1));
        }
    }
}

