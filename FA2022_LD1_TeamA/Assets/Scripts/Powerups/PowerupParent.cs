using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParent : MonoBehaviour
{
    public Price ShopCost;
    public string ItemName;
    public string ItemDescription;

    private void Awake()
    {
        Instantiate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && ShopCost.CanPlayerAfford())
        {
            Debug.Log("PowerupParent onTriggerEnter");
            ActivatePowerup(collision);
            GameObject g = Instantiate(GameManager.Instance.PowerupUIPrefab);
            g.GetComponent<PowerupUIControl>().Activate(ItemName, ItemDescription);
            Destroy(gameObject);
        }   
    }

    public virtual void ActivatePowerup(Collision collision)
    {
        Debug.Log("Using PowerupParent");
    }

    public virtual void Instantiate()
    {
        ShopCost.Cost = 0;
    }
}
