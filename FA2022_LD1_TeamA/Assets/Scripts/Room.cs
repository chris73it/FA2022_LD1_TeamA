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
        RegularRoom 
    }
    public RoomTypes Type { get; set; }
    public int Depth;
    public static int Height = 3;
    public List<Room> ConnectedRooms = new List<Room>();
    public int Choices = 2; // might be random later?
    // Door
    public List<GameObject> PickUps; // PickUps
    public bool IsCleared = false;
    // array or list that holds number of entities in a room in the room
    private string _roomName;
    public string RoomName
    {
        get {
            //Debug.Log(Room.RoomTypes.GetName(typeof(Room.RoomTypes), Type)); 
            return Room.RoomTypes.GetName(typeof(Room.RoomTypes), Type); }
    }

    //private bool _loadingRoom = false;

    // Constructors
    public Room()
    {
        Type = getRandomType();
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

    // Sets the Depth of the room to depth, then fills the Room's Connections with a new Room. It calls
    // itself to fill the new Room's connections. This continues until depth is equal to the Height of the
    // Room class, then adds the Boss room to the current Room's Connections. Repeats until all of the Room's
    // Connections are filled properly
    public void GenerateRooms(int depth)
    {
        Depth = depth;

        if (depth < Height)
        {
            for (int i = 0; i < Choices; i++)
            {
                Room r = new Room(RoomTypes.EmptyRoom); // testing with only empty rooms

                // Rooms are emtpy
                ConnectedRooms.Add(r);
                //Debug.Log("Count: " + ConnectedRooms.Count);

                r.GenerateRooms(++depth);
            }
        } else if (depth == Height)
        {
            //ConnectedRooms.Add(Boss room);
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

        spawnRoomItems();
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
            if (o[i].GetComponent<Spawner>().Type == Spawner.SpawnerTypes.Door)
            {
                if (IsCleared)
                {
                    //Debug.Log("Spawning...");
                    go = o[i].GetComponent<Spawner>().InstantiateObject();
                    
                    //Debug.Log(go.name);

                    go.GetComponent<RoomTransport>().NextRoom = ConnectedRooms[choicesCounter];
                    choicesCounter++;
                }
            } else
            {
                o[i].GetComponent<Spawner>().InstantiateObject();
            }
        }          
    }
}
