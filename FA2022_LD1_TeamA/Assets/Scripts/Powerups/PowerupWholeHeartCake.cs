using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupWholeHeartCake : PowerupParent
{
    public override void ActivatePowerup(Collision collision)
    {
        collision.gameObject.GetComponent<Health>().MaxHealth += 1;
        collision.gameObject.GetComponent<Health>().Heal(1);
        Debug.Log("Your max health increased by 1");
    }
}
