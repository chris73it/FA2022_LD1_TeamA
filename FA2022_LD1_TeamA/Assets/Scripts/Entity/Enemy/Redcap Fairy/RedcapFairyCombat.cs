using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedcapFairyCombat : EnemyCombat
{

    public RedcapFairyMovement Movement;

    public override void Initialize()
    {
        base.Initialize();
        SightRange = 10f;
        Damage = 1;
        AttackRange = 2f;
        AttackCooldown = 3f;
        Movement = GetComponent<RedcapFairyMovement>();
    }

    public override void Attack()
    {
        AttackDirection = GetEnemyDirection();
        AttackDelay = 0.3f;
        IsAttacking = true;
        Animator.SetTrigger("Attacking");
    }

    public void Update()
    {

        if (IsPlayerInRange(AttackRange))
        {
            if (Movement.State != RedcapFairyMovement.RedcapFairyStates.Attacking)
            {
                Movement.State = RedcapFairyMovement.RedcapFairyStates.Attacking;
            }

            if (AttackCooldown <= 0 && !IsAttacking)
            {
                Attack();
                Movement.Destination = transform.position;
            }
        } else if (IsPlayerInRange(SightRange))
        {
            if (Movement.State != RedcapFairyMovement.RedcapFairyStates.Pursuit)
            {
                Movement.State = RedcapFairyMovement.RedcapFairyStates.Pursuit;
                //Debug.Log("Pursuit!");
            }
        } else
        {
           if (Movement.State != RedcapFairyMovement.RedcapFairyStates.Wander && Movement.State != RedcapFairyMovement.RedcapFairyStates.Wander)
            {
                Movement.State = RedcapFairyMovement.RedcapFairyStates.Wander;
            }
        }

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        BaseTimers();
    }

    void OnDrawGizmos()
    {
        if (IsAttacking)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(AttackDirection + transform.position, new Vector3(1, 1, 1));
        }
    }
}
