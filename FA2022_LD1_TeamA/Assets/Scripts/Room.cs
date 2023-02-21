using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room
{
    /// Properties

    // Room List
    public enum RoomTypes // Make sure names are same as Scene names
    {
        StartingRoom,
        EmptyRoom,
        RegularRoom,
        RegularRoom1,
        RegularRoom2,
        RegularRoom3,
        BossRoom,
        ShopRoom,
    }

    // Type
    public RoomTypes Type { get; set; }

    // Position
    public int Row = -1;
    public int Column = -1;

    // Size
    // 1 small, 2 medium, 3 large
    public int Width;
    public int Height;

    // Name
    public string RoomName
    {
        get
        {
            return Room.RoomTypes.GetName(typeof(Room.RoomTypes), Type);
        }
    }

    // State
    private bool _isCleared = false;
    public bool IsCleared
    {
        get
        {
            return _isCleared;
        }
        set
        {
            _isCleared = value;
            //spawnRoomItems();
            

            if (Reward != null)
            {
                spawnReward();
            }

            foreach (GameObject door in DoorsSpawned)
            {
                door.GetComponent<RoomTransport>().Active = true;
            }

            //Debug.Log("ISCLEARED");
            // activate doors, spawn powerup
        }
    }

    public bool HasEntered = false;

    // Reward
    public Transform RewardLocation;
    public GameObject Reward = null;

    // Connected Rooms
    // Indices: 0 and 1 North, 2 and 3 West, 4 and 5 South, 6 and 7 East
    public Room[] ConnectedRooms = new Room[8];

    // Room Manager
    public RoomManager Manager;

    /// GameObjects in Room
    // Enemies
    public List<GameObject> EnemiesSpawned = new List<GameObject>();
    // Doors
    public List<GameObject> DoorsSpawned = new List<GameObject>(); // Could change DoorsSpawned to an array of 8 since there will always only ever be 8 doors

    /// Methods 

    // Constructors

    public Room(int row, int column, int start = 1, int end = 6, int width = 2, int height = 2, bool cleared = false)
    {
        Type = getRandomType(start, end);
        Row = row;
        Column = column;
        Width = width;
        Height = height;
        Manager = new RoomManager(this);
        isAlwaysClearedRoom(cleared);
    }

    public Room(RoomTypes type, int row, int column, int width = 1, int height = 1, bool cleared = false)
    {
        Type = type;
        Row = row;
        Column = column;
        Width = width;
        Height = height;
        Manager = new RoomManager(this);
        isAlwaysClearedRoom(cleared);
    }

    // Pre Generation Related
    private static RoomTypes getRandomType(int start = 0, int end = 2) // end is exclusive
    {
        return (RoomTypes)Random.Range(start, end); // might have to cast to int first?
    }

    private void isAlwaysClearedRoom(bool cleared)
    {
        if (Type == RoomTypes.EmptyRoom || Type == RoomTypes.ShopRoom)
        {
            IsCleared = true;
        }
        else
        {
            IsCleared = cleared;
        }
    }

    // Room Generation
    public void SetReward()
    {
        if (Type != RoomTypes.EmptyRoom || Type != RoomTypes.StartingRoom)
        {
            if (FloorManager.Instance.ForestPowerupsPool.Count > 0)
            {
                int rewardIndex = Random.Range(0, FloorManager.Instance.ForestPowerupsPool.Count);
                Reward = FloorManager.Instance.ForestPowerupsPool[rewardIndex];
                FloorManager.Instance.ForestPowerupsPool.RemoveAt(rewardIndex);
                //Debug.Log("Powerup Count: " + FloorManager.Instance.ForestPowerupsPool.Count);
            }
        }
    }

    // Post Generation Related
    public void OnRoomEnter()
    {
        //Debug.Log("StartCoroutine");
        //_loadingRoom = true;
        //SceneManager.LoadScene(RoomName, LoadSceneMode.Single);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(RoomName));
        //Debug.Log("Active Scene: " + SceneManager.GetActiveScene().name);

        //Debug.Log("Active Scene: " + SceneManager.GetActiveScene().name);
        EnemiesSpawned.Clear();

        if (!HasEntered)
        {
            Debug.Log("Obtaining Room Entities");
            getAllRoomEntities();
        }
        else
        {
            //Debug.Log("Spawning Entities");
            Manager.SpawnRoomEntities();
        }

        // spawnRoomItems();

        int DoorDirection = -1;

       if (FloorManager.LastDoorDirection < 4)
        {
            DoorDirection = FloorManager.LastDoorDirection + 4;
        } else
        {
            DoorDirection = FloorManager.LastDoorDirection - 4;
        }

        foreach (GameObject door in DoorsSpawned)
        {
            if (door != null && door.transform.GetChild(1).GetComponent<Spawner>().Direction == DoorDirection)
            {
                GameManager.ChosenPlayerCharacter.transform.position = door.transform.GetChild(1).transform.position; // Enabling Auto Sync Transforms in Physics settings
            }
        }

        HasEntered = true;
    }

    private void getAllRoomEntities()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");

        foreach (GameObject o in spawners)
        {
            o.GetComponent<Spawner>().AddObjectToRoom();
        }
    }

    private void spawnReward()
    {
        // create a gameobject spawner with type powerup that adds itself to this room
        GameObject.Instantiate(Reward, RewardLocation);
    }

    private void spawnRoomItems()
    {
        //Debug.Log(GameObject.FindGameObjectsWithTag("Spawner").Length);   
        // Always spawn pickups
        // Only spawn enemies if not cleared
        // only spawn doors if cleared
        GameObject[] o = GameObject.FindGameObjectsWithTag("Spawner");
        int choicesCounter = 0;
        GameObject go;

        for (int i = 0; i < o.Length; i++)
        {

            if (o[i].GetComponent<Spawner>().Type == Spawner.SpawnerTypes.Door) // maybe turn this all into a switch later
            {
                if (IsCleared)
                {
                    /*
                    if (choicesCounter < Choices && ConnectedRooms.Count > 0)
                    {
                        //Debug.Log("Spawning...");
                        go = o[i].GetComponent<Spawner>().InstantiateObject();
                        //Debug.Log(go.name);
                        //Rooms currently dont spawn anything since they the rest are not cleared
                        go.GetComponent<RoomTransport>().NextRoom = ConnectedRooms[choicesCounter];
                        choicesCounter++;
                    }
                    */
                    go = o[i].GetComponent<Spawner>().InstantiateObject();
                }
            }
            else if (o[i].GetComponent<Spawner>().Type == Spawner.SpawnerTypes.Enemy && !IsCleared)
            {
                go = o[i].GetComponent<Spawner>().InstantiateObject();
                EnemiesSpawned.Add(go);
            }
            else if (o[i].GetComponent<Spawner>().Type == Spawner.SpawnerTypes.Powerup && IsCleared)
            {
                if (Reward != null)
                {
                    o[i].GetComponent<Spawner>().ToSpawn = Reward;
                    o[i].GetComponent<Spawner>().InstantiateObject();
                }
                else
                {
                    Debug.Log("No reward");
                }
            }
            else if (o[i].GetComponent<Spawner>().Type != Spawner.SpawnerTypes.Enemy && o[i].GetComponent<Spawner>().Type != Spawner.SpawnerTypes.Powerup) // && not Reward? then u can deal with the reward stuff in another if
            {
                o[i].GetComponent<Spawner>().InstantiateObject();
            }
            // else if reward and is cleared
            // set to spawn to reward then instantiate
        }
    }
}
