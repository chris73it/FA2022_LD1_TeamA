using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfCombat : Combat
{
    public float AttackDistance;
    public float AttackRadius;

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
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.y = AttackerTransform.up.y;
        return mousePos;
    }
    public override void Attack()
    {
        Collider[] Damaged = Physics.OverlapBox(AttackerTransform.position + getAttackDistance(), new Vector3(AttackRadius, AttackRadius, AttackRadius));
        if (Damaged.Length > 0)
        {
            for (int i = 0; i < Damaged.Length; i++)
            {
                //Debug.Log(Damaged[i]);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
       Gizmos.DrawWireCube(AttackerTransform.position + getAttackDistance(), new Vector3(AttackRadius, AttackRadius, AttackRadius));
    }


}
