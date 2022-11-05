using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLogic : MonoBehaviour
{
    public enum PickupTypes
    {
        Coin,
        Health,
        Stamina
    }
    public PickupTypes Type;
    public int Value = 0;
    public Price ShopCost;
    public bool Active = false;

    private void Awake()
    {
        RandomizeType();

        ShopCost.Cost = 0;

        Value = Random.Range(1, 3);

        Active = true;
    }
    public void RandomizeType()
    {
        var pickupTypesLength = PickupTypes.GetNames(typeof(PickupTypes)).Length;
        Type = (PickupTypes)Random.Range(0, pickupTypesLength);
    }

    public void RandomizeType(int start)
    {
        var pickupTypesLength = PickupTypes.GetNames(typeof(PickupTypes)).Length;
        Type = (PickupTypes)Random.Range(start, pickupTypesLength);
    }

    public void RandomizeType(int start, int end)
    {
        var pickupTypesLength = PickupTypes.GetNames(typeof(PickupTypes)).Length;
        Type = (PickupTypes)Random.Range(start, end);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Active)
        {
            if (collision.gameObject.tag == "Player" && ShopCost.CanPlayerAfford())
            {
                switch (Type)
                {
                    case PickupTypes.Coin:
                        collision.gameObject.GetComponent<Wealth>().Deposit(Value);
                        break;

                    case PickupTypes.Health:
                        collision.gameObject.GetComponent<Health>().Heal(Value);
                        break;

                    case PickupTypes.Stamina:
                        collision.gameObject.GetComponent<PlayerMovement>().RestoreStamina(1f);
                        break;

                    default:
                        break;
                }

                Debug.Log("You got " + Value + " " + PickupTypes.GetName(typeof(PickupTypes), Type));
                Destroy(gameObject);
            }
        }
    }    
}
