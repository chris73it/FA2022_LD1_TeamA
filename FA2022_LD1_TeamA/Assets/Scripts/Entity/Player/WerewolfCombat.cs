using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfCombat : Combat
{
    public float AttackDistance;
    public float AttackRadius;
    public float offsetScaleX;
    public float offsetScaleZ;
    public float ChargeCooldown;
    public Vector3 ChargeDirection;
    public bool Charging;
    public bool Charged;
    public float ChargeAttackTime;
    public int Combo;
    public float ComboCooldown;

    void Start()
    {
        offsetScaleX = 8.4f;
        offsetScaleZ = 8.4f;
        ChargeCooldown = 0f;
        Charging = false;
        Charged = false;
        ChargeAttackTime = 0f;
        Combo = 0;
        ComboCooldown = 0f;
    }
    void Update()
    {
        BaseTimers();

        if (IsStunned <= 0f)
        {
            if (AttackCooldown <= 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (Combo == 0)
                    {
                        Attack();
                    }
                    else if (Combo == 1)
                    {
                        Attack2();
                    }
                    else if (Combo == 2)
                    {
                        Attack3();
                    }
                }

                if (Input.GetButtonDown("Fire2") && GameManager.ChosenPlayerCharacter.GetComponent<PlayerMovement>().CurrentStamina > 0.25f)
                {
                    GameManager.ChosenPlayerCharacter.GetComponent<PlayerMovement>().UseStamina(0.25f);
                    ChargeAttack();
                }
            }
        }
        
        if (ChargeCooldown > 0)
        {
            ChargeCooldown -= Time.deltaTime;
            controller.Move(ChargeDirection * Time.deltaTime * 10);
            if (ChargeCooldown <= 0)
            {
                AttackRadius = 3;
                Charged = true;
                ChargeAttackTime = 0.2f;
                Collider[] Damaged = Physics.OverlapBox(AttackerTransform.position, new Vector3(AttackRadius, AttackRadius, AttackRadius));
                if (Damaged.Length > 0)
                {
                    OnDamage(Damaged);
                }
            }
        }

        if (ComboCooldown > 0)
        {
            ComboCooldown -= Time.deltaTime;
        }
        if (ComboCooldown <= 0)
        {
            Combo = 0;
        }

        if (ChargeCooldown > 0)
        {
            ChargeCooldown -= Time.deltaTime;
            controller.Move(ChargeDirection * Time.deltaTime * 10);
        }
        if (ChargeCooldown <= 0 && Charging == true)
        {
            AttackRadius = 3;
            Charging = false;
            Charged = true;
            ChargeAttackTime = 0.2f;
            Collider[] Damaged = Physics.OverlapBox(AttackerTransform.position, new Vector3(AttackRadius, AttackRadius, AttackRadius));
            if (Damaged.Length > 0)
            {
                OnDamage(Damaged);
            }
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
                Damaged[i].gameObject.GetComponent<Combat>().OnDamageAnimation(); // redundant???
                
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
                            Debug.Log("Stunned!");
                        }
                    }
                }
            }
        }
    }
    private Vector3 getAttackDistance()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + 1;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.y = 0f;
        mousePos.z = mousePos.z * offsetScaleZ;
        mousePos.x = mousePos.x * offsetScaleX;
        mousePos.z -= AttackerTransform.position.z;
        mousePos.x -= AttackerTransform.position.x;
        mousePos.z *= 2;
        mousePos.x *= 2;
        float radius = Mathf.Sqrt(Mathf.Pow(mousePos.z, 2) + Mathf.Pow(mousePos.x, 2));
        mousePos.x = mousePos.x / radius * 2;
        mousePos.z = mousePos.z / radius * 2;

        Vector3 distance = mousePos;
        return distance;
    }
    private Vector3 getEnemyDirection(Vector3 enemyDistance)
    {
        enemyDistance.z -= AttackerTransform.position.z;
        enemyDistance.x -= AttackerTransform.position.x;
        float radius = Mathf.Sqrt(Mathf.Pow(enemyDistance.z, 2) + Mathf.Pow(enemyDistance.x, 2));
        enemyDistance.z = enemyDistance.z / radius * 2;
        enemyDistance.x = enemyDistance.x / radius * 2;
        return enemyDistance;
    }
    public override void Attack()
    {
        Debug.Log("Attack");

        AttackRadius = 1f;
        Collider[] Damaged = Physics.OverlapSphere(AttackerTransform.position + getAttackDistance(), AttackRadius);
        if (Damaged.Length > 0)
        {
            OnDamage(Damaged);
        }
        Combo = 1;
        ComboCooldown = 2f;
    }
    public void Attack2()
    {
        Debug.Log("Attack");

        AttackRadius = 2f;
        Collider[] Damaged = Physics.OverlapBox(AttackerTransform.position + getAttackDistance(), new Vector3(AttackRadius, AttackRadius, AttackRadius));
        if (Damaged.Length > 0)
        {
            OnDamage(Damaged);
        }
        Combo = 2;
        ComboCooldown = 2f;
    }
    public void Attack3()
    {
        Debug.Log("Attack");
        AttackRadius = 2f;
        Collider[] Damaged = Physics.OverlapSphere(AttackerTransform.position + (getAttackDistance() * 1.5f), AttackRadius);
        if (Damaged.Length > 0)
        {
            OnDamage(Damaged);
        }
        Combo = 3;
        ComboCooldown = 1f;
    }
    public override void ChargeAttack()
    {
        ChargeDirection = getAttackDistance();
        ChargeCooldown = 0.4f;
        Charging = true;
    }
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

        if (Charged == true)
        {
            Gizmos.DrawWireSphere(AttackerTransform.position, 3);
            ChargeAttackTime -= Time.deltaTime;
            if (ChargeAttackTime <= 0)
            {
                Charged = false;
            }
        }
        else if (Combo == 0)
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



