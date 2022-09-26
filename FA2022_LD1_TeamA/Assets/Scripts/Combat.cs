using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Transform AttackerTransform;
    public int Damage;
    public float Invulnerability { get; set; }
    public bool IsAttacking = false;

    
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


    public void ChargeAttack() 
    {
        Debug.Log("Using Base Combat");
    }
}
