using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : TrapParent
{
    // Time interval
    public float TriggerTime = 2.5f; //1.5 seconds

    // Timer
    public float ToggleTimer = 0f;

    // Damage
    public bool CanDamage = false;

    public override void Initialize()
    {
        Damage = 1;
    }

    private void Update()
    {
        if (ToggleTimer > TriggerTime)
        {
            CanDamage = !CanDamage;
            ToggleTimer = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CanDamage)
        {

        }
    }
}
