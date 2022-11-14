using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupElectricClaws : PowerupParent
{
    public override void ActivatePowerup(Collision collision)
    {
        collision.gameObject.GetComponent<Combat>().StunChance = 0.9f;
        collision.gameObject.GetComponent<Combat>().StunTimer = 1f;

        /// deal with attackers using stun
        /// deal with stun effects
    }
}
