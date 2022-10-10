using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfCombat : Combat
{
    public float AttackDistance;
    public float AttackRadius;
    public float offsetScaleX;
    public float offsetScaleZ;

    void Start()
    {
        offsetScaleX = 8.4f;
        offsetScaleZ = 8.4f;
    }
    void Update()
    {
        if (Invulnerability > 0)
        {
            Invulnerability -= Time.deltaTime;
            //Debug.Log("Invulnerability: " + Invulnerability);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
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
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackerTransform.position + getAttackDistance(), 1);
    }


}