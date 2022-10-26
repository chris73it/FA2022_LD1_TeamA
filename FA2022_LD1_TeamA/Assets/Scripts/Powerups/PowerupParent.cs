using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParent : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PowerupParent onTriggerEnter");
            ActivatePowerup(collision);
            Destroy(gameObject);
        }   
    }

    public virtual void ActivatePowerup(Collision collision)
    {
        Debug.Log("Using PowerupParent");
    }
}
