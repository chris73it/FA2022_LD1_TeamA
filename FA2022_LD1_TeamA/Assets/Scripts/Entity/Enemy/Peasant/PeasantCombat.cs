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
        AttackRange = 3f;
        Movement = GetComponent<PeasantMovement>();
    }

    public override void Attack()
    {
        AttackDirection = getEnemyDirection();
        AttackDelay = 0.3f;
        IsAttacking = true;
        SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);
    }

    private void Update()
    {

        if (IsPlayerInRange(AttackRange))
        {
            if (Movement.State != PeasantMovement.PeasantStates.Attacking)
            {
                Movement.State = PeasantMovement.PeasantStates.Attacking;
            }

            if (AttackCooldown <= 0 && !IsAttacking)
            {
                Attack();
            }

            if (AttackDelay <= 0 && IsAttacking)
            {
                Damage = 1;
                Collider[] Damaged = Physics.OverlapBox(AttackDirection + transform.position, new Vector3(1, 1, 1));
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
                AttackCooldown = 1f;
                AttackResult = 1f;
            }
        }
        else if (IsPlayerInRange(SightRange))
        {
            if (Movement.State != PeasantMovement.PeasantStates.Pursuit)
            {
                Movement.State = PeasantMovement.PeasantStates.Pursuit;
            }
        }
        else
        {
            if (Movement.State != PeasantMovement.PeasantStates.Wander)
            {
                Movement.State = PeasantMovement.PeasantStates.Wander;
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
    private Vector3 getEnemyDirection()
    {
        Vector3 playerDistance;
        playerDistance = (Player.transform.position - transform.position);
        float radius = Mathf.Sqrt(Mathf.Pow(playerDistance.z, 2) + Mathf.Pow(playerDistance.x, 2));
        playerDistance.z = playerDistance.z / radius * 2;
        playerDistance.x = playerDistance.x / radius * 2;
        return playerDistance;
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
            Gizmos.DrawWireCube(getEnemyDirection() + transform.position, new Vector3(1, 1, 1));
        }
    }
}

