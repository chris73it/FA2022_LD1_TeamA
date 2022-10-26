using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPoisonClaws : PowerupParent
{
public override void ActivatePowerup(Collision collision)
    {
        collision.gameObject.GetComponent<Combat>().DoT = new Combat.DamageOverTime();
        collision.gameObject.GetComponent<Combat>().DoT.Damage = 1;
        collision.gameObject.GetComponent<Combat>().DoT.TimeToDamage = 1f;
        collision.gameObject.GetComponent<Combat>().DoT.TotalDuration = 3f;
        collision.gameObject.GetComponent<Combat>().DoT.Timer = 0f;

        Debug.Log("You now have Poison DoT");
    }
}
