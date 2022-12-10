using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPoisonClaws : PowerupParent
{
public override void ActivatePowerup(Collision collision)
    {
        collision.gameObject.GetComponent<Combat>().DoT = new Combat.DamageOverTime(1, 1f, 3f, 0f);

        //Debug.Log("You now have Poison DoT");
    }

    public override void Instantiate()
    {
        base.Instantiate();

        ItemName = "Poison Claws";
        ItemDescription = "Attacks deal damage over time!";
    }
}
