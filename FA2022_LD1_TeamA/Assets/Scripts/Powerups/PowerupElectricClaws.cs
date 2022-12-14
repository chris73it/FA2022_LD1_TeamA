using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupElectricClaws : PowerupParent
{
    public override void ActivatePowerup(Collision collision)
    {
        collision.gameObject.GetComponent<Combat>().StunChance = 0.9f;
        collision.gameObject.GetComponent<Combat>().StunTimer = 1f;
        collision.gameObject.GetComponent<Combat>().Animator.SetInteger("AttackType", 2);

        //Debug.Log("You now have a stun!");
        /// deal with attackers using stun
        /// deal with stun effects
    }

    public override void Instantiate()
    {
        base.Instantiate();

        ItemName = "Electric Claws";
        ItemDescription = "Attacks may stun enemies";
    }
}
