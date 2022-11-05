using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParent : MonoBehaviour
{
    public Price ShopCost;

    private void Awake()
    {
        ShopCost.Cost = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && ShopCost.CanPlayerAfford())
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
