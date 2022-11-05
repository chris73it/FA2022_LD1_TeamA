using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ToSpawn;
    public enum SpawnerTypes
    {
        Pickup,
        Powerup,
        Door,
        Enemy,
        Player
    }
    public SpawnerTypes Type;

    public GameObject InstantiateObject()
    {
        if (Type != SpawnerTypes.Player)
        {
            GameObject o = Instantiate(ToSpawn, gameObject.transform.position, gameObject.transform.rotation);
            
            if (FloorManager.CurrentRoom.Type == Room.RoomTypes.ShopRoom)
            {
                if (Type == SpawnerTypes.Powerup)
                {
                    o.GetComponent<PowerupParent>().ShopCost.Cost = 15;
                    Debug.Log(o.GetComponent<PowerupParent>().ShopCost.Cost);
                } else if (Type == SpawnerTypes.Pickup)
                {
                    o.GetComponent<PickupLogic>().ShopCost.Cost = 3;

                    if (o.GetComponent<PickupLogic>().Type == PickupLogic.PickupTypes.Coin)
                    {
                        o.GetComponent<PickupLogic>().RandomizeType(1,2);
                    }

                    Debug.Log(o.GetComponent<PickupLogic>().ShopCost.Cost);
                }
            }

            return o;
        }

        return null;
    }
}
