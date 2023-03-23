using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfCombat : Combat
{

    // HEAVY ATTACKS, COMBOS

    /// General
    // How far an attack hitbox should be from the player
    public float AttackDistance;

    // How big the attack's hitbox is
    public float AttackRadius;

    /// Input Logger
    // Input Types
    public enum AttackTypes
    {
        Light1,
        Light2,
        Light3,
        Heavy1,
        Heavy2,
        Heavy3,
        Dash,
    }

    // Stores the attack inputs of the player
    public List<AttackTypes> AttackInputs;

    // How long an Input lasts before Input Log is emptied
    public float InputTimer;

    // Initial value for InputTimer
    public const float InputDuration = 3f;

    /// Dash 
 
    // How long before a player can dash again
    public float DashCooldown;

    // How long a dash should take
    public float DashTimeInitial;

    // How far a dash should go
    public float DashDistance;

    // Which direction a dash should happen
    public Vector3 DashDirection;

    // Dash cancelled because of obstacles or something else
    public bool DashCancelled;

    // How long until the Dash ends
    public float DashTime;

    /// Combo
    // Current Combo counter
    public int Combo;

    // How long until the combo ends
    public float ComboCooldown;

    void Start()
    {
        DashCooldown = 0f;
        DashTimeInitial = 0.1f;
        DashCancelled = false;
        DashTime = 0f;
        DashDistance = 5f;
        Combo = 0;
        AttackInputs = new List<AttackTypes>();
        ComboCooldown = 0f;
    }
    void Update()
    {
        BaseTimers();

        // Inputs
        if (IsStunned <= 0f)
        {
            if (AttackCooldown <= 0)
            {
                // Light Attack
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

                // Heavy Attack
                if (Input.GetButtonDown("HeavyAttack"))
                {
                    Animator.SetTrigger("Attacking");

                }
            }

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
        // Attack
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

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

        // Inputs
        if (AttackInputs.Count > 0 && InputTimer > 0f)
        {
            InputTimer -= Time.deltaTime;

            if (InputTimer <= 0f)
            {
                AttackInputs.Clear();
                InputTimer = InputDuration;
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
                AttackInputs.Add(AttackTypes.Dash);
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
        Debug.Log("Light Attack 1");

        SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);

        AttackRadius = 1f;
        Collider[] Damaged = Physics.OverlapSphere(AttackerTransform.position + getAttackDistance(), AttackRadius);
        if (Damaged.Length > 0)
        {
            OnDamage(Damaged);
        }
        Combo = 1;
        AttackCooldown = 0.25f;
        ComboCooldown = 2f;
        AttackInputs.Add(AttackTypes.Light1);
    }
    public void Light2()
    {
        Debug.Log("Light Attack 2");

        SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);

        AttackRadius = 2f;
        Collider[] Damaged = Physics.OverlapBox(AttackerTransform.position + getAttackDistance(), new Vector3(AttackRadius, AttackRadius, AttackRadius));
        if (Damaged.Length > 0)
        {
            OnDamage(Damaged);
        }
        Combo = 2;
        AttackCooldown = 0.25f;
        ComboCooldown = 2f;
        AttackInputs.Add(AttackTypes.Light2);
    }
    public void Light3()
    {
        Debug.Log("Light Attack 3");

        SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);

        AttackRadius = 2f;
        Collider[] Damaged = Physics.OverlapSphere(AttackerTransform.position + (getAttackDistance() * 1.5f), AttackRadius);
        if (Damaged.Length > 0)
        {
            OnDamage(Damaged);
        }
        Combo = 3;
        AttackCooldown = 0.25f;
        ComboCooldown = AttackCooldown;
        AttackInputs.Add(AttackTypes.Light3);
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



