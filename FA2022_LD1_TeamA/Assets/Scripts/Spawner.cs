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
            int price = 0;

            if (FloorManager.CurrentRoom.Type == Room.RoomTypes.ShopRoom)
            {
                if (Type == SpawnerTypes.Powerup)
                {
                    price = 15;
                    o.GetComponent<PowerupParent>().ShopCost.Cost = price;
                    Debug.Log(o.GetComponent<PowerupParent>().ShopCost.Cost);
                } else if (Type == SpawnerTypes.Pickup)
                {
                    price = 3;
                    o.GetComponent<PickupLogic>().ShopCost.Cost = price;

                    if (o.GetComponent<PickupLogic>().Type == PickupLogic.PickupTypes.Coin)
                    {
                        o.GetComponent<PickupLogic>().RandomizeType(1,2);
                    }

                    Debug.Log(o.GetComponent<PickupLogic>().ShopCost.Cost);
                }

                GameObject t = Instantiate(GameManager.Instance.TextObject, 
                    o.transform.position + GameManager.Instance.TextObject.transform.position, GameManager.Instance.TextObject.transform.rotation);

                t.transform.parent = o.transform;
                t.GetComponent<TextMesh>().text = "" + price + "¢";
            }

            return o;
        }

        return null;
    }
}
