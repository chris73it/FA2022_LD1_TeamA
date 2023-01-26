using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    // Properties

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
            
            if (_isCleared) {
                spawnRoomItems();
            }
        }
    }

    // Reward
    public GameObject Reward = null;

    // Connected Rooms
    // Indices: 0 North, 1 West, 2 South, 3 East
    public Room[] ConnectedRooms = new Room[4];

    // Enemies
    public List<GameObject> EnemiesSpawned = new List<GameObject>();

    // Methods 

    // Constructors

    public Room(int start = 0, int end = 2, int row, int column, int width = 1, int height = 1, bool cleared = false)
    {
        Type = getRandomType(start, end);
        Row = row;
        Column = column;
        Width = width;
        Height = height;
        isCleared = cleared;
    }

    public Room (RoomTypes type, int row, int column, int width = 1, int height = 1, bool cleared = false) {
        Type = type;
        Row = row;
        Column = column;
        Width = width;
        Height = height;
        isCleared = cleared;
    }

    // 
    private static RoomTypes getRandomType(int start = 0, int end = 2) // end is exclusive
    {
        return (RoomTypes)Random.Range(start, end); // might have to cast to int first?
    }

    // Room Generating Related

    public Room[] GenerateRoom(int roomsToGenerate)
    {
        int roomsGenerated = 0;
        Room[,] floor = new Room[FloorManager.Width, FloorManager.Height];

        // variables needed:
            // amount of rooms to generate

        // Spawner can have position variable to determine which door connects to what

        // place starting room if first floor
        // check if rooms to generate has been reached
            // set prize of current room if applicable
            // choose any random room
                // choose random direction of room
                    // make sure direction does not already have a room
                    // add new room to current room's connected room
                    // set room's index
                    // set room's size
                    // room.generate()
        // if rooms to generate has been reached
            // choose random room location
            // add boss room and shop room to a valid location

        /// Place Starting Floor

        return floor;
    }

    private void setReward()
    {

    }

    // Post Generation Related
    public void OnRoomEnter()
    {

    }
    private void spawnRoomItems()
    {

    }
}
