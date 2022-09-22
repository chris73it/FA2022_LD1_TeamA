using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int Damage;
    public float Invulnerability { get; set; }
    

    private void Update()
    {
        // suppose invlunerable for 3 seconds
            // if using frames to determine timer, then the timer for thirty seconds would be different for each pc
            // if using delta.Time, same case, since a frame can take longer than 3 seconds before loading

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
}
