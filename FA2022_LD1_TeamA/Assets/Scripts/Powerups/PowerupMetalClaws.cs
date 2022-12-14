using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMetalClaws : PowerupParent
{
    public override void ActivatePowerup(Collision collision)
    {
        collision.gameObject.GetComponent<Combat>().Damage += 1;
        collision.gameObject.GetComponent<Combat>().Animator.SetInteger("AttackType", 3);
        //Debug.Log("Damage is now " + collision.gameObject.GetComponent<Combat>().Damage);
    }

    public override void Instantiate()
    {
        base.Instantiate();

        ItemName = "Metal Claws";
        ItemDescription = "+1 to Damage";
    }
}

