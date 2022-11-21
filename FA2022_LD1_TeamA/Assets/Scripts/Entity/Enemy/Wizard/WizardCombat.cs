using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WizardCombat : EnemyCombat
{
    public WizardMovement Movement;
    public float AttackResult = 0f;

    public override void Initialize()
    {
        base.Initialize();
        SightRange = 12f;
        AttackRange = 10f;
        RetreatRange = 5f;
        Movement = GetComponent<WizardMovement>();
    }

    public override void Attack()
    {
        AttackDestination = Player.transform.position;
        AttackDelay = 0.5f;
        IsAttacking = true;
    }

    private void Update()
    {

        if (IsPlayerInRange(RetreatRange))
        {
            if (Movement.State != WizardMovement.WizardStates.Retreat)
            {
                Movement.State = WizardMovement.WizardStates.Retreat;
            }
        }
        else if (IsPlayerInRange(AttackRange))
        {
            if (Movement.State != WizardMovement.WizardStates.Attacking)
            {
                Movement.State = WizardMovement.WizardStates.Attacking;
            }

            if (AttackCooldown <= 0 && !IsAttacking)
            {
                Attack();
            }

            if (AttackDelay <= 0 && IsAttacking)
            {
                Damage = 1;
                Collider[] Damaged = Physics.OverlapSphere(AttackDestination, 0.5f);
                if (Damaged.Length > 0)
                {
                    for (int i = 0; i < Damaged.Length; i++)
                    {
                        if (Damaged[i].gameObject.tag == "Player")
                        {
                            Damaged[i].gameObject.GetComponent<Health>().TakeDamage(Damage);
                            Debug.Log("Hit");
                        }
                    }
                }
                IsAttacking = false;
                AttackCooldown = 2f;
                AttackResult = 1f;
            }
        }
        else if (IsPlayerInRange(SightRange))
        {
            if (Movement.State != WizardMovement.WizardStates.Pursuit)
            {
                Movement.State = WizardMovement.WizardStates.Pursuit;
            }
        }
        else
        {
            if (Movement.State != WizardMovement.WizardStates.Wander)
            {
                Movement.State = WizardMovement.WizardStates.Wander;
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
            Gizmos.DrawWireSphere(AttackDestination, 0.5f);
        }
        else if (AttackResult > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(AttackDestination, 0.5f);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Player.transform.position, 0.5f);
        }
    }
}

