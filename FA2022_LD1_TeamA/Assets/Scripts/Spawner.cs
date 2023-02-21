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
        Obstacles,
        Player
    }
    public SpawnerTypes Type;
    public int Direction = -1;
    public int Price = 0;

    public GameObject InstantiateObject()
    {
        if (Type != SpawnerTypes.Player)
        {
            GameObject o = Instantiate(ToSpawn, gameObject.transform.position, gameObject.transform.rotation);
            int price = 0;

            if (FloorManager.CurrentRoom.Type == Room.RoomTypes.ShopRoom)
            {
                if (Type == SpawnerTypes.Powerup || Type == SpawnerTypes.Pickup)
                {
                    if (Type == SpawnerTypes.Powerup)
                    {
                        price = 15;
                        o.GetComponent<PowerupParent>().ShopCost.Cost = price;
                        Debug.Log(o.GetComponent<PowerupParent>().ShopCost.Cost);
                    }
                    else
                    {
                        price = 3;
                        o.GetComponent<PickupLogic>().ShopCost.Cost = price;

                        Debug.Log(o.GetComponent<PickupLogic>().ShopCost.Cost);
                    }

                    GameObject t = Instantiate(GameManager.Instance.TextObject,
                    o.transform.position + GameManager.Instance.TextObject.transform.position, GameManager.Instance.TextObject.transform.rotation);

                    t.transform.parent = o.transform;
                    t.GetComponent<TextMesh>().text = "" + price + "$";
                }
               /* else if (Type == SpawnerTypes.Door)
                {
                    o.GetComponent<RoomTransport>().NextFloor = true;
                }
               */
            }/*
            else
            {
                if (Type == SpawnerTypes.Door)
                {
                    o.GetComponent<RoomTransport>().SetRoom(Direction);
                }
            }
            */

            return o;
        }

        return null;
    }

    public GameObject AddObjectToRoom()
    {
        if (ToSpawn != null)
        {
            GameObject o = Instantiate(ToSpawn, gameObject.transform.position, gameObject.transform.rotation);

            if (Type == SpawnerTypes.Enemy)
            {
                FloorManager.CurrentRoom.EnemiesSpawned.Add(o);
            } else if (Type == SpawnerTypes.Pickup)
            {
                if (Price > 0)
                {
                    o.GetComponent<PickupLogic>().ShopCost.Cost = Price;
                }
            }

            return o;
        } else
        {
            if (Type == SpawnerTypes.Powerup)
            {
                FloorManager.CurrentRoom.RewardLocation = gameObject.transform;

                if (Price > 0)
                {
                    FloorManager.CurrentRoom.Reward.GetComponent<PowerupParent>().ShopCost.Cost = Price;
                }
            }
        }

        return null;
    }
}
