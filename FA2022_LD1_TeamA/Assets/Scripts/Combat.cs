using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Transform AttackerTransform;
    public CharacterController controller;
    public int Damage;
    public bool IsAttacking = false;
    public Vector3 AttackDestination;
    public Vector3 AttackDirection;
    public float AttackDelay = 0f;
    public float AttackCooldown = 0f;
    public List<GameObject> Bullets;
    public List<GameObject> BulletsCreated;
    public float Invulnerability { get; set; }

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

    // Animation Info
    public SpriteRenderer Sprite;
    public float DamagedDuration;
    public float DamagedTimer;
    public float InvulnerabilityDuration;

    // Check if overriden by derived classes, needs Invulnerability section    
    private void Awake()
    {
        DamagedDuration = 0.25f;
        DamagedTimer = 0f;
    }
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

        if (DamagedTimer > 0f)
        {
            DamagedTimer -= Time.deltaTime;
        } else
        {
            Sprite.color = Color.white;
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

    public virtual void OnDamage(Collider[] Damaged) // This should be used but gameObject always returns a null for some reason
    {
        Debug.Log("Using Base Combat");
    }

    public void OnDamageAnimation()
    {
        Sprite.color = Color.red;
        DamagedTimer = DamagedDuration;
    }
}
