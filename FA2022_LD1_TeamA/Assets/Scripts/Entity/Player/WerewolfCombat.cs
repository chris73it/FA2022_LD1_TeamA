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
    public bool Charged;
    public float ChargeAttackTime;

    void Start()
    {
        offsetScaleX = 8.4f;
        offsetScaleZ = 8.4f;
        ChargeCooldown = 0f;
        Charged = false;
        ChargeAttackTime = 0f;
    }
    void Update()
    {
        if (Invulnerability > 0)
        {
            Invulnerability -= Time.deltaTime;
            //Debug.Log("Invulnerability: " + Invulnerability);
        }

        if (AttackCooldown <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }

            if (Input.GetButtonDown("Fire2") && GameManager.ChosenPlayerCharacter.GetComponent<PlayerMovement>().CurrentStamina > 0.25f)
            {
                GameManager.ChosenPlayerCharacter.GetComponent<PlayerMovement>().UseStamina(0.25f);
                ChargeAttack();
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
                    for (int i = 0; i < Damaged.Length; i++)
                    {
                        if (Damaged[i].gameObject.tag == "Enemy")
                        {
                            Damaged[i].gameObject.GetComponent<Health>().TakeDamage(Damage);
                        }
                    }
                }
            }
        }

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
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
    public override void Attack()
    {
        AttackRadius = 1;
        Collider[] Damaged = Physics.OverlapBox(AttackerTransform.position + getAttackDistance(), new Vector3(AttackRadius, AttackRadius, AttackRadius));
        if (Damaged.Length > 0)
        {
            for (int i = 0; i < Damaged.Length; i++)
            {
                if (Damaged[i].gameObject.tag == "Enemy")
                {
                    Damaged[i].gameObject.GetComponent<Health>().TakeDamage(Damage);
                }
            }
        }
        AttackCooldown = 0.3f;
        //Debug.Log("Attack");
    }
    public override void ChargeAttack()
    {
        ChargeDirection = getAttackDistance();
        ChargeCooldown = 0.4f;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Charged == true)
        {
            Gizmos.DrawWireSphere(AttackerTransform.position, 3);
            ChargeAttackTime -= Time.deltaTime;
            if (ChargeAttackTime <= 0)
            {
                Charged = false;
            }
        }
        else
        {
            Gizmos.DrawWireSphere(AttackerTransform.position + getAttackDistance(), 1);
        }

    }


}
