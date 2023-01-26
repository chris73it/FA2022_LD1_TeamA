using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager : MonoBehaviour
{
    // Properties

    // Instance
    public static FloorManager Instance = null;

    // Floor Map
    public static int Height = 3;
    public static int Width = 3;
    public Room[,] Floor = new Room[Height, Width];

    // Floor Attributes
    public enum FloorTypes
    {
        Base,
        Forest
    }
    public FloorTypes Type = FloorTypes.Base;

    public bool NextFloor = false;
    public static int RoomsEntered = 0;
    public static int FloorsCompleted = 0;
    public static int RoomsToGenerate = 10;
    public static int BaseRoomTreeHeight = 4; // deprecated 
    public static int RoomTreeHeight = BaseRoomTreeHeight; // deprecated 

    // Powerup List
    public List<GameObject> ForestPowerupsList; // Does not reset on a game over restart
    public List<GameObject> ForestPowerupsPool; 

    //public static List<GameObject> PickupsList;
    //public static List<GameObject> EnemiesList;

    // Room Switching
    private static Room _currentRoom;
    public static Room CurrentRoom 
    {
        get 
        { 
            return _currentRoom; 
        }
        set
        {
            _currentRoom = value;

            SceneManager.LoadScene(_currentRoom.RoomName, LoadSceneMode.Single);
        }
    }

    // Static Rooms
    public static Room StartingFloor { get; set; }
    public static Room BossRoom;
    public static Room ShopRoom;

    // Init:
    // Check if instance exists already, if not set instance = this;
    // Determine Floor Type, FloorSize
    // Flood Floor with Rooms with Type == RoomsType.Nothing
    // Choose one random room (as a start, make sure it is an empty default room)
    // Generate other rooms based off that
    private void Awake()
    {
        // Intialize instance if null
        if (Instance == null)
        {
            Instance = this;
        }
        //Debug.Log(Instance);

        SceneManager.sceneLoaded += this.onLoadCallback;
        //SceneManager.SetActiveScene()

        //ResetFloor();

        //string roomName = Room.RoomTypes.GetName(typeof(Room.RoomTypes), StartingFloor.Type);
        //SceneManager.LoadScene(roomName, LoadSceneMode.Single);
    }

    private void Update() // to be removed
    {
        /*
        if (Input.GetButtonDown("Fire2"))
        {
            CurrentRoom.IsCleared = true;
        }
        */
    }
    // Methods
    public void ResetFloor()
    {
        Debug.Log("Resetting...");
        // Reset Room Entered Count
        RoomsEntered = 0;
        ForestPowerupsPool = new List<GameObject>(ForestPowerupsList);

        // Switch to next Floor Type
        /*
        if (newFloor)
        {
            Type++; // This is not necessarily true for every ResetFloor() call, i.e. a game over should send the player to the first floor type
        } else
        {
            Type = FloorTypes.Forest;
        }
        */

        Type = FloorTypes.Forest; // redundant ^
        if (NextFloor)
        {
            Debug.Log("RoomTreeHeight++");
            RoomTreeHeight++;
            FloorsCompleted++;
        } else
        {
            FloorsCompleted = 0;
            RoomTreeHeight = BaseRoomTreeHeight;
        }

        /*
        // Generate Static Rooms
        StartingFloor = new Room(Room.RoomTypes.StartingRoom);
        BossRoom = new Room(Room.RoomTypes.BossRoom);
        ShopRoom = new Room(Room.RoomTypes.ShopRoom);
        ShopRoom.IsCleared = true;
        ShopRoom.ConnectedRooms.Add(new Room());

        Room.Height = RoomTreeHeight;

        // Generate All Rooms
        StartingFloor.GenerateRooms(0);
        */
    }

    private void onLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name != "MainMenu")
        {
            CurrentRoom.OnRoomEnter();
            Debug.Log(RoomsEntered + ": " + CurrentRoom.RoomName);
            RoomsEntered++;
        } else
        {
            Debug.Log("Active Scene: " + SceneManager.GetActiveScene().name);
        } 
    }
      // generate starting room
      // generate its connections
      // repeat until for loop reaches height,
      // then make all last rooms connect to boss
}
