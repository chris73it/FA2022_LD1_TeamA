using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room
{
    public Scene SceneRoom { get; set; } // Might just have to be a string
    public enum RoomTypes // Make sure names are same as Scene names
    {
        EmptyRoom,
        RegularRoom,
        BossRoom,
        ShopRoom, 
    }
    public RoomTypes Type { get; set; }
    public int Depth;
    public static int Height;
    public List<Room> ConnectedRooms = new List<Room>();
    public int Choices = 2; // might be random later?
    // Door
    public List<GameObject> PickUps; // PickUps
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
            spawnRoomItems();
        } 
    }
    // array or list that holds number of entities in a room in the room
    public string RoomName
    {
        get 
        {
            //Debug.Log(Room.RoomTypes.GetName(typeof(Room.RoomTypes), Type)); 
            return Room.RoomTypes.GetName(typeof(Room.RoomTypes), Type); 
        }
    }

    //private bool _loadingRoom = false;

    // Constructors
    public Room()
    {
        Type = getRandomType();
    }

    public Room(int start, int end)
    {
        Type = getRandomType(start, end);
    }
    public Room(RoomTypes t)
    {
        Type = t;
    }

    // Methods
    // Takes a random scene from the scene list and returns it
    private static Scene getRandomScene()
    {
        var randomScene = Random.Range(0, SceneManager.sceneCountInBuildSettings - 1);
        return new Scene();//SceneManager.GetSceneByBuildIndex(randomScene);
    }

    // Gets a random enum RoomTypes and returns it
    private static RoomTypes getRandomType()
    {
        var roomTypesLength = RoomTypes.GetNames(typeof(RoomTypes)).Length;
        return (RoomTypes)Random.Range(0, roomTypesLength); // might have to cast to int first?
    }

    private static RoomTypes getRandomType(int start = 0, int end = 2) // end is not inclusive
    {
        return (RoomTypes)Random.Range(start, end); // might have to cast to int first?
    }

    // Sets the Depth of the room to depth, then fills the Room's Connections with a new Room. It calls
    // itself to fill the new Room's connections. This continues until depth is equal to the Height of the
    // Room class, then adds the Boss room to the current Room's Connections. Repeats until all of the Room's
    // Connections are filled properly
    public void GenerateRooms(int depth)
    {
        Depth = depth;

        if (depth == Height - 1)
        {
            Choices = 1;
            Room b = new Room(RoomTypes.BossRoom);
            ConnectedRooms.Add(b);
            b.GenerateRooms(++depth);

        } else if (depth < Height)
        {
            for (int i = 0; i < Choices; i++)
            {
                //Room r = new Room(0, 2);

                Room r = new Room(RoomTypes.RegularRoom);

                // Rooms are emtpy
                ConnectedRooms.Add(r);
                //Debug.Log("Count: " + ConnectedRooms.Count);

                r.GenerateRooms(++depth);
            }
        } else
        {
            Choices = 1;
            ConnectedRooms.Add(new Room(RoomTypes.ShopRoom));
        }
    }

    public void OnRoomEnter() 
    {
        //Debug.Log("StartCoroutine");
        //_loadingRoom = true;
        //SceneManager.LoadScene(RoomName, LoadSceneMode.Single);

        //Debug.Log(IsCleared);
        

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(RoomName));
        //Debug.Log("Active Scene: " + SceneManager.GetActiveScene().name);

        //Debug.Log("Active Scene: " + SceneManager.GetActiveScene().name);

        spawnRoomItems();

        GameObject player = GameObject.FindWithTag("Player");
        GameObject[] o = GameObject.FindGameObjectsWithTag("Spawner");

        foreach (GameObject element in o)
        {
            if (element.GetComponent<Spawner>().Type == Spawner.SpawnerTypes.Player)
            {
                //Debug.Log("Player: " + player.name);
                //player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = element.transform.position; // Enabling Auto Sync Transforms in Physics settings
                //player.GetComponent<CharacterController>().enabled = true;

            }
        }
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
                    if (choicesCounter < Choices && ConnectedRooms.Count > 0)
                    {
                        //Debug.Log("Spawning...");
                        go = o[i].GetComponent<Spawner>().InstantiateObject();
                        //Debug.Log(go.name);
                        //Rooms currently dont spawn anything since they the rest are not cleared
                        go.GetComponent<RoomTransport>().NextRoom = ConnectedRooms[choicesCounter];
                        choicesCounter++;
                    }   
                }
            } else if (o[i].GetComponent<Spawner>().Type == Spawner.SpawnerTypes.Enemy && !IsCleared)
            {
                o[i].GetComponent<Spawner>().InstantiateObject();
            } else if (o[i].GetComponent<Spawner>().Type != Spawner.SpawnerTypes.Enemy)
            {
                o[i].GetComponent<Spawner>().InstantiateObject();
            }

        }          
    }
}
