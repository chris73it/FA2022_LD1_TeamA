using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMetalClaws : PowerupParent
{
    public override void ActivatePowerup(Collision collision)
    {
        collision.gameObject.GetComponent<Combat>().Damage += 1;
        Debug.Log("Damage is now " + collision.gameObject.GetComponent<Combat>().Damage);
    }
}

