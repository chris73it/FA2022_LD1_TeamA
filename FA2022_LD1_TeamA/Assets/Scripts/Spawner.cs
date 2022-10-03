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
        Enemy
    }
    public SpawnerTypes Type;

    public GameObject InstantiateObject()
    {
        return Instantiate(ToSpawn, gameObject.transform.position, gameObject.transform.rotation);
    }
}
