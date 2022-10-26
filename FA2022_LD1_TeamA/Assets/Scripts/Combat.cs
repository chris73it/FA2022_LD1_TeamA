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
    public float IsStunned = 0f;

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
        if (Invulnerability > 0)
        {
            Invulnerability -= Time.deltaTime;
            Debug.Log("Invulnerability: " + Invulnerability);
        }
    }

    // Used for damage and etc.
    public void SetGeneralInvulnerability(float seconds)
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
}
