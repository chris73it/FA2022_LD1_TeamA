using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Transform AttackerTransform;
    public CharacterController controller;
    public int Damage;
    public float AttackCooldown = 0f;
    public float Invulnerability { get; set; }
    public float IsAttacking = 0f;

    private float isStunned = 0f;
    public float IsStunned
    {
        get { return isStunned; }
        set 
        { 
            isStunned = value;
            // do animations
        }
    } // Determines how long a person is stunned for
    public float StunTimer = 0f; // Determines how long the attacker's stunning lasts for
    public float StunChance = 0f; // Determines how often a stun attack lands

    public struct DamageOverTime
    {
        public int Damage;
        public float TotalDuration;
        public float TimeToDamage;
        public float Timer;
    }

    public DamageOverTime DoT;

    // Check if overriden by derived classes, needs Invulnerability section    
    private void Update()
    {
        BaseTimers();
    }

    public void BaseTimers()
    {
        if (Invulnerability > 0f)
        {
            Invulnerability -= Time.deltaTime;
            //Debug.Log("Invulnerability: " + Invulnerability);
        }

        if (IsStunned > 0f)
        {
            IsStunned -= Time.deltaTime;
        }
    }
    // Used for damage and etc.
    public void SetGeneralInvulnerability(float seconds) // can be changed to a property
    {
        Invulnerability = seconds;

        // do animations.
    }

    public virtual void Attack()
    {
        Debug.Log("Using Base Combat");
    }


    public virtual void ChargeAttack()
    {
        Debug.Log("Using Base Combat");
    }

    public void OnDamage(Collider[] Damaged)
    {
        for (int i = 0; i < Damaged.Length; i++)
        {
            if (Damaged[i].gameObject.tag == "Enemy" || Damaged[i].gameObject.tag == "Obstacle")
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
}
