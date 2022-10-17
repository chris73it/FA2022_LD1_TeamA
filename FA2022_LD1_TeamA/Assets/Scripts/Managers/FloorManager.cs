using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager : MonoBehaviour
{
    // Instance
    public static FloorManager Instance = null;

    // Floor Attributes
    public enum FloorTypes
    {
        Base,
        Forest
    }

    public FloorTypes Type = FloorTypes.Base;

    //public static List<Powerups> PowerupsList;
    //public static List<Pickups> PickupsList;
    //public static List<Enemies> EnemiesList;

    public static int RoomsEntered = 0;
    public static int RoomTreeHeight = 5;
    public static Room StartingFloor { get; set; }
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
        // Reset Room Entered Count
        RoomsEntered = 0;

        // Switch to next Floor Type
        Type++; // This is not necessarily true for every ResetFloor() call, i.e. a game over should send the player to the first floor type

        // Generate Static Rooms
        StartingFloor = new Room(Room.RoomTypes.EmptyRoom);
        BossRoom = new Room(Room.RoomTypes.BossRoom);
        ShopRoom = new Room(Room.RoomTypes.ShopRoom);

        Room.Height = RoomTreeHeight;

        // Generate All Rooms
        StartingFloor.GenerateRooms(0);
    }

    private void onLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        CurrentRoom.OnRoomEnter();
        Debug.Log(RoomsEntered + ": " + CurrentRoom.RoomName);
        RoomsEntered++;
    }
      // generate starting room
      // generate its connections
      // repeat until for loop reaches height,
      // then make all last rooms connect to boss
}
