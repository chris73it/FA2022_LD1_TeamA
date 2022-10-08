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

        Debug.Log(Instance);

        SceneManager.sceneLoaded += this.onLoadCallback;
        //SceneManager.SetActiveScene()

        //ResetFloor();

        //string roomName = Room.RoomTypes.GetName(typeof(Room.RoomTypes), StartingFloor.Type);
        //SceneManager.LoadScene(roomName, LoadSceneMode.Single);
    }

    private void Update() // to be removed
    {
        if (Input.GetButtonDown("Fire2"))
        {
            CurrentRoom.IsCleared = true;
        }
    }
    // Methods
    public void ResetFloor()
    {
        // Switch to next Floor Type
        Type++; // ?

        // Generate Starting Room
        StartingFloor = new Room(Room.RoomTypes.EmptyRoom);
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
