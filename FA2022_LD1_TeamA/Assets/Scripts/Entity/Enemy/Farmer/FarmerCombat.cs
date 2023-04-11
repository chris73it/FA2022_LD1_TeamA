using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FarmerCombat : EnemyCombat
{
    public FarmerMovement Movement;

    public override void Initialize()
    {
        base.Initialize();
        AttackRange = 10f;
        Movement = GetComponent<FarmerMovement>();
        AttackCooldown = 3f;
    }

    public override void Attack()
    {
        Animator.SetTrigger("Attacking");
        CreateBullet(Damage, 3.5f, true);

        AttackCooldown = 2f;
    }

    private void Update()
    {
        if (IsStunned <= 0f)
        {
            if (IsPlayerInRange(AttackRange))
            {
                if (Movement.State != FarmerMovement.FarmerStates.Attacking)
                {
                    Movement.State = FarmerMovement.FarmerStates.Attacking;
                }

                if (AttackCooldown <= 0)
                {
                    Attack();
                }
            }
            else
            {
                if (Movement.State != FarmerMovement.FarmerStates.Wander)
                {
                    Movement.State = FarmerMovement.FarmerStates.Wander;
                }
            }
        }

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        BaseTimers();
    }
}
