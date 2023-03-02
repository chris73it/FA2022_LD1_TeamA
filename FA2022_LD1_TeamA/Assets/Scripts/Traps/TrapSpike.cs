using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : TrapParent
{
    // Active Duration
    public float ActiveDuration = 3f;

    // Active Timer
    public float ActiveTimer = 0f;

    // Size
    public float Size = 0.25f;

    // CanDamage
    public bool CanDamage = false;

    public override void Initialize()
    {
        Damage = 1;
        Size *= transform.parent.transform.localScale.x;
    }

    private void Update()
    {
        if (ActiveTimer > ActiveDuration)
        {
            if (!CanDamage)
            {
                Animator.SetTrigger("Attack");
                ActiveTimer = 0;
            } else
            {
                FlipCanDamage();
                ActiveTimer = 0;
                Animator.SetTrigger("FinishAttack");
            }
        } else
        {
            ActiveTimer += Time.deltaTime;

            if (CanDamage)
            {
                Attack();
            }
        }
    }

    public void FlipCanDamage()
    {
        CanDamage = !CanDamage;
    }

    public void Attack()
    {
        Collider[] Damaged = Physics.OverlapSphere(transform.position, Size);

        for (int i = 0; i < Damaged.Length; i++)
        {
            if (Damaged[i].gameObject.tag == "Player")
            {
                // Damage
                Debug.Log(Damaged[i].name);
                Damaged[i].gameObject.GetComponent<Health>().TakeDamage(Damage);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (CanDamage)
        {
            Gizmos.DrawWireSphere(transform.position, Size);
        }
    }
}
