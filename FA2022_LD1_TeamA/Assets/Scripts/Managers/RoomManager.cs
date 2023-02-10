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
    public List<GameObject> PowerupsSpawned = new List<GameObject>();

    // Obstacles
    public List<GameObject> ObstaclesSpawned = new List<GameObject>();

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
            }
  
        } else
        {
            Debug.Log("PickupsSpawned empty");
        }
        /*
        // Powerup
        if (PowerupsSpawned.Count > 0 && Owner.IsCleared)
        {
            foreach (GameObject powerup in PowerupsSpawned)
            {
                GameObject.Instantiate(powerup); // this doesnt work bc powerups arent stored in spawners
            }
        }

        

        // Obstacles
        if (ObstaclesSpawned.Count > 0)
        {
            foreach (GameObject obstacle in ObstaclesSpawned)
            {
                GameObject.Instantiate(obstacle);
            }
        }

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
