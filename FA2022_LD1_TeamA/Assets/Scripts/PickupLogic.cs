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
    public bool Active = false;

    // Start is called before the first frame update
    void Start()
    {

        var pickupTypesLength = PickupTypes.GetNames(typeof(PickupTypes)).Length;
        //Type = (PickupTypes)Random.Range(0, pickupTypesLength); // Stamina has no use yet
        Type = (PickupTypes)Random.Range(0, pickupTypesLength - 1);

        Value = Random.Range(1, 3);

        Active = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Active)
        {
            if (collision.gameObject.tag == "Player")
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
