using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackCombat : EnemyCombat
{
    public LumberjackMovement Movement;
    public float SlamRadius;
    public float AttackRadius;
    public override void Initialize()
    {
        base.Initialize();
        AttackRadius = 3f;
        AttackCooldown = 3f;
        SlamRadius = 4f;
        Movement = GetComponent<LumberjackMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if (IsStunned <= 0f)
        {
            if (Movement.State == LumberjackMovement.LumberjackStates.Attacking)
            {
                if (BulletsCreated.Count <= 1)
                {
                    if (AttackCooldown <= 0)
                    {
                        Attack();
                        AttackCooldown = 1f;
                    }
                } 
            }
        }

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        BaseTimers();
    }

    public override void Attack()
    {
        CreateBullet(Damage, 2.5f, true);
    }

    /*
    public void SlamAttack()
    {
        Collider[] Damaged = Physics.OverlapBox(AttackerTransform.position, new Vector3(SlamRadius, SlamRadius, SlamRadius));
        OnDamage(Damaged);       
    }

    public override void OnDamage(Collider[] Damaged)
    {
        for (int i = 0; i < Damaged.Length; i++)
        {
            if (Damaged[i].gameObject.tag == "Player" || Damaged[i].gameObject.tag == "Obstacle")
            {
                // Damage
                Damaged[i].gameObject.GetComponent<Health>().TakeDamage(Damage);

                // Stun
                if (StunTimer > 0f)
                {
                    if (StunChance > 0f)
                    {
                        if (Random.Range(0f, 1f) >= StunChance)
                        {
                            Damaged[i].gameObject.GetComponent<Combat>().IsStunned = StunTimer;
                            Debug.Log("Stunned!");
                        }
                    }
                }
            }
        }
    }
    */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SlamRadius);
    }
}
