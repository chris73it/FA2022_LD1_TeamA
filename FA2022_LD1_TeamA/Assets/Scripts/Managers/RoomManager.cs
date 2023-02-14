using System.Collections.Generic;
using UnityEngine;


/// Holds pickups, obstacles, and powerups to spawn them properly
public class RoomManager
{
    // Room
    public Room Owner;

    // Pickups Prefab
    public static GameObject PickupPrefab;

    // Pickups
    public List<(PickupLogic, Vector3, Quaternion)> PickupsSpawned = new List<(PickupLogic, Vector3, Quaternion)>();

    // Powerups
    public List<(PowerupParent, Vector3, Quaternion)> PowerupsSpawned = new List<(PowerupParent, Vector3, Quaternion)>();

    // Obstacles
    public List<(int, Vector3, Quaternion)> ObstaclesSpawned = new List<(int, Vector3, Quaternion)>();

    // Traps
    public List<GameObject> TrapsSpawned = new List<GameObject>();

    public RoomManager(Room owner)
    {
        Owner = owner;
    }

    public void SpawnRoomEntities()
    {
        GameObject o;

        // Pickup
        if (PickupsSpawned.Count > 0)
        {
            Debug.Log("PickupsSpawned not empty");

            foreach ((PickupLogic, Vector3, Quaternion) pickup in PickupsSpawned.ToArray())
            {
                o = GameObject.Instantiate(FloorManager.Instance.PickupPrefab, pickup.Item2, pickup.Item3); //breaks because when one is spawned, it adds to the list again
                
                o.GetComponent<PickupLogic>().Type = pickup.Item1.Type;
                o.GetComponent<PickupLogic>().Value = pickup.Item1.Value; 
                o.GetComponent<PickupLogic>().ShopCost = pickup.Item1.ShopCost; 
            }

            PickupsSpawned.Clear();
        } else
        {
            Debug.Log("PickupsSpawned empty");
        }

        // Powerup
        if (PowerupsSpawned.Count > 0)
        {
            foreach ((PowerupParent, Vector3, Quaternion) powerup in PowerupsSpawned)
            {
                GameObject.Instantiate(FloorManager.Instance.PowerupPrefabs[powerup.Item1.Index], powerup.Item2, powerup.Item3); 
            }

            PowerupsSpawned.Clear();
        } else
        {
            Debug.Log("PowerupsSpawned empty");
        }

        // Obstacles
        if (ObstaclesSpawned.Count > 0)
        {
            foreach ((int, Vector3, Quaternion) obstacle in ObstaclesSpawned)
            {
                GameObject.Instantiate(FloorManager.Instance.ObstaclePrefabs[obstacle.Item1], obstacle.Item2, obstacle.Item3);
            }
        }

        /*
        // Traps
        if (TrapsSpawned.Count > 0)
        {
            foreach (GameObject trap in TrapsSpawned)
            {
                GameObject.Instantiate(trap);
            }
        }
    */
    }

}
