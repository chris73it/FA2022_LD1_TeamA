using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfCombat : Combat
{
    public float AttackDistance;
    public float AttackRadius;
    public float DashCooldown;
    public float DashTimeInitial;
    public float DashDistance;
    public Vector3 DashDirection;
    public bool DashCancelled;
    public float DashTime;
    public int Combo;
    public float ComboCooldown;

    void Start()
    {
        DashCooldown = 0f;
        DashTimeInitial = 0.1f;
        DashCancelled = false;
        DashTime = 0f;
        DashDistance = 5f;
        Combo = 0;
        ComboCooldown = 0f;
    }
    void Update()
    {
        BaseTimers();

        // Inputs
        if (IsStunned <= 0f)
        {
            // Light Attack
            if (AttackCooldown <= 0)
            {
                if (Input.GetButtonDown("LightAttack"))
                {
                    Animator.SetTrigger("Attacking");
                    if (Combo == 0)
                    {
                        Light1();
                    }
                    else if (Combo == 1)
                    {
                        Light2();
                    }
                    else if (Combo == 2)
                    {
                        Light3();
                    }
                }
            }

            // Heavy Attack

            // Dash
            if (DashCooldown <= 0)
            {
                if (Input.GetButtonDown("Dash") && GameManager.ChosenPlayerCharacter.GetComponent<PlayerMovement>().CurrentStamina > 0.25f)
                {
                    //Animator.SetTrigger("Attacking");
                    GameManager.ChosenPlayerCharacter.GetComponent<PlayerMovement>().UseStamina(0.25f);
                    SetGeneralInvulnerability(GetComponent<Health>().HitInvulnerability);
                    Dash();
                    DashTime = DashTimeInitial;
                }
            }
        }
        
        /// Cooldowns
        // Dash
        if (DashCooldown > 0)
        {
            DashCooldown -= Time.deltaTime;
        }

        // Combo
        if (ComboCooldown > 0)
        {
            ComboCooldown -= Time.deltaTime;

            if (ComboCooldown <= 0)
            {
                Combo = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        // Dashing
        if (DashTime > 0)
        {
            DashTime -= Time.deltaTime;
            controller.Move((DashDistance/DashTimeInitial) * Time.deltaTime * DashDirection);

            // check for obstacles? if touching an obstacle, dashtime = 0;

            if (DashTime <= 0 || DashCancelled)
            {
                DashCancelled = false;
                DashTime = 0;
                DashCooldown = 1.25f;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Cancel Dash if player collides with Obstacle
        if (DashTime > 0)
        {
            DashCancelled = hit.collider.gameObject.CompareTag("Obstacle");
        }
    }

    public override void OnDamage(Collider[] Damaged)
    {
        for (int i = 0; i < Damaged.Length; i++)
        {
            if (Damaged[i].gameObject.tag == "Enemy" || Damaged[i].gameObject.tag == "Obstacle")
            {
                // Damage
                Damaged[i].gameObject.GetComponent<Health>().TakeDamage(Damage); 
               
                if (DoT.Intialized)
                {
                    Damaged[i].gameObject.GetComponent<Health>().DoT = DoT;
                    Damaged[i].gameObject.GetComponent<Health>().IsDoT = true;
                }
               
                // Stun
                if (StunTimer > 0f)
                {
                    if (StunChance > 0f)
                    {
                        if (Random.Range(0f, 1f) >= StunChance)
                        {
                            Damaged[i].gameObject.GetComponent<Combat>().IsStunned = StunTimer;
                            //Debug.Log("Stunned!");
                        }
                    }
                }
            }
        }
    }
    private Vector3 getAttackDistance()
    {
        Vector3 attackPos = new Vector3(0, 0, 0);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 300))
        {
            attackPos = hit.point;
            attackPos -= GameManager.ChosenPlayerCharacter.transform.position;
            attackPos.y = 0;
            attackPos.Normalize();
            attackPos *= AttackDistance;
        }

        return attackPos;
    }

    // Light Attacks
    public void Light1()
    {
        Debug.Log("Attack");

        SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);

        AttackRadius = 1f;
        Collider[] Damaged = Physics.OverlapSphere(AttackerTransform.position + getAttackDistance(), AttackRadius);
        if (Damaged.Length > 0)
        {
            OnDamage(Damaged);
        }
        Combo = 1;
        ComboCooldown = 2f;
    }
    public void Light2()
    {
        Debug.Log("Attack");
        SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);

        AttackRadius = 2f;
        Collider[] Damaged = Physics.OverlapBox(AttackerTransform.position + getAttackDistance(), new Vector3(AttackRadius, AttackRadius, AttackRadius));
        if (Damaged.Length > 0)
        {
            OnDamage(Damaged);
        }
        Combo = 2;
        ComboCooldown = 2f;
    }
    public void Light3()
    {
        Debug.Log("Attack");
        SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);

        AttackRadius = 2f;
        Collider[] Damaged = Physics.OverlapSphere(AttackerTransform.position + (getAttackDistance() * 1.5f), AttackRadius);
        if (Damaged.Length > 0)
        {
            OnDamage(Damaged);
        }
        Combo = 3;
        ComboCooldown = 1f;
    }
    
    // Dash
    public override void Dash()
    {
        DashDirection = getAttackDistance();
        DashDirection.Normalize();
        SoundSource.PlayOneShot(SoundClips[1], GameManager.Instance.SoundVolume / 10f);
    }

    // Gizmos
    void OnDrawGizmos()
    {
        if (Combo == 3)
        {
            Gizmos.color = Color.blue;
        }
        else if (ComboCooldown <= 0)
        {
            Gizmos.color = Color.red;
        }
        else if (ComboCooldown <= 1)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        if (Combo == 0)
        {
            Gizmos.DrawWireSphere(AttackerTransform.position + getAttackDistance(), 1);
        }
        else if (Combo == 1)
        {
            Gizmos.DrawWireCube(AttackerTransform.position + getAttackDistance(), new Vector3(2, 2, 2));
        }
        else if (Combo == 2 || Combo == 3)
        {
            Gizmos.DrawWireSphere(AttackerTransform.position + (getAttackDistance() * 1.5f), 2);
        }
    }
}



