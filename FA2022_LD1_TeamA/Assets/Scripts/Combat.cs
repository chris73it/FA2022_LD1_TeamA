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
    public float Invulnerability;

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
        public bool Intialized;

        public DamageOverTime(int d, float td, float ttd, float t)
        {
            Damage = d;
            TotalDuration = td;
            TimeToDamage = ttd;
            Timer = t;
            Intialized = true;
        }
    }

    public DamageOverTime DoT;

    // Animation Info
    public SpriteRenderer Sprite;
    public float DamagedDuration;
    public float DamagedTimer;
    public float InvulnerabilityAlpha;

    // Check if overriden by derived classes, needs Invulnerability section    
    private void Awake()
    {
        AnimationInitialization();
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
            if (Invulnerability <= 0f)
            {
                InvulernabilityAnimation();
            }
        }

        if (IsStunned > 0f)
        {
            IsStunned -= Time.deltaTime;
        }

        if (DamagedTimer > 0f)
        {
            DamagedTimer -= Time.deltaTime;

            if (DamagedTimer <= 0f)
            {
                Sprite.color = new Color(1,1,1, Sprite.color.a);
            }
        } 
    }
    // Used for damage and etc.
    public void SetGeneralInvulnerability(float seconds) // can be changed to a property
    {
        Invulnerability = seconds;
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
        if (GetComponent<Health>().IsDoT)
        {
            Sprite.color = new Color(0, 1, 0, Sprite.color.a);
        } else
        {
            Sprite.color = new Color(1, 0, 0, Sprite.color.a);
        }
        DamagedTimer = DamagedDuration;
    }

    public void InvulernabilityAnimation()
    {
        Color c = Sprite.color;

        if (Invulnerability > 0f)
        {
            c.a = InvulnerabilityAlpha;
        } else
        {
            c.a = 1f;
        }
        
        Sprite.color = c;
    }


    public void AnimationInitialization()
    {
        DamagedDuration = 0.25f;
        DamagedTimer = 0f;
        InvulnerabilityAlpha = 0.5f;
        Sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
}
