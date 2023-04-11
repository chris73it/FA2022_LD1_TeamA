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
    public static int MapRows = 10;
    public static int MapColumns = 10;
    public Room[,] Floor = new Room[MapRows, MapColumns];

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

    // Powerup List
    public List<GameObject> ForestPowerupsList; // Does not reset on a game over restart
    public List<GameObject> ForestPowerupsPool;

    // Prefabs
    public GameObject PickupPrefab;
    public List<GameObject> PowerupPrefabs;
    public List<GameObject> ObstaclePrefabs;

    // Room Switching
    private static Room currentRoom;
    public static Room CurrentRoom 
    {
        get 
        { 
            return currentRoom; 
        }
        set
        {
            PreviousRoom = currentRoom;
            currentRoom = value;

            SceneManager.LoadScene(currentRoom.RoomName, LoadSceneMode.Single);
        }
    }

    public static Room PreviousRoom;
    public static int LastDoorDirection = -1;

    // Static Rooms
    public static Room StartingRoom { get; set; }
    public static Room BossRoom;
    public static Room ShopRoom;

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

    /// Methods

    // Room Generating Related
    public void ResetFloor()
    {
        Debug.Log("Resetting...");
        // Reset Room Entered Count
        RoomsEntered = 0;
        ForestPowerupsPool = new List<GameObject>(ForestPowerupsList);

        /// Create starting room
        StartingRoom = createStartingRoom();

        /// Add Starting Room
        Floor[StartingRoom.Row, StartingRoom.Column] = StartingRoom;

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

        // Next Floor Rules
        if (NextFloor)
        {
            // Debug.Log("RoomTreeHeight++");
            // RoomTreeHeight++;
            FloorsCompleted++;
        } else
        {
            FloorsCompleted = 0;
            //RoomTreeHeight = BaseRoomTreeHeight;
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

        GenerateRoom(RoomsToGenerate);

        //Debug.Log(Floor);
    }

    private Room createStartingRoom()
    {
        return new Room(Room.RoomTypes.StartingRoom, Random.Range(1, MapRows - 2), Random.Range(1, MapColumns - 2), 2, 2, true);
    }

    public void GenerateRoom(int roomsToGenerate)
    {
        // Room info
        int width = 2; // change sizes later
        int height = 2;

        // Filled Rooms
        List<(int, int)> filledRooms = new List<(int, int)>();

        // Valid Room Positions
        List<(int, int)> validPositions = new List<(int, int)>();

        // Room to check Valid Positions
        (int, int) check = (-1, -1);

        // Valid Position
        (int, int) validPosition = (-1, -1);

        // Adjacent Room
        //Room adjacentRoom;

        // variables needed:
        // amount of rooms to generate

        // Spawner can have position variable to determine which door connects to what

        // place starting room if first floor
        // check if rooms to generate has been reached
        // choose any random room
        // choose random direction of room
        // make sure direction does not already have a room
        // add new room to current room's connected room
        // set room's index
        // set room's size
        // set prize of current room if applicable
        // if rooms to generate has been reached
        // choose random room location
        // add boss room and shop room to a valid location

        /// Get StartingRoom's position and add to filledRooms
        filledRooms.Add((StartingRoom.Row, StartingRoom.Column));
        
        /// Room Generation (do it the other way around? start with boss room and shop room connected then build it around that?)
        for (int i = 0; i < roomsToGenerate; i++)
        {
            /// Check latest room added
            check = filledRooms[filledRooms.Count - 1];

            /// Add valid positions
            validPositions = addValidRooms(validPositions, filledRooms, check);

            /// Choose random valid position
            if (validPositions.Count > 0)
            {
                validPosition = validPositions[Random.Range(0, validPositions.Count)];
            }

            Debug.Log("Position Chosen: " + validPosition);

            /// Create new room at valid position   
            if (i == roomsToGenerate - 1)
            {
                // Place Shop Room
                /*
                foreach ((int, int) v in validPositions)
                {
                    Debug.Log(v);
                }*/

                Floor[validPosition.Item1, validPosition.Item2] = new Room(Room.RoomTypes.ShopRoom, validPosition.Item1, validPosition.Item2, width, height, false);
                ShopRoom = Floor[validPosition.Item1, validPosition.Item2];
            }
            else if (i == roomsToGenerate - 2)
            {
                // Place Boss Room
                Debug.Log("Adding Boss Room" + validPosition.Item1 + " " + validPosition.Item2);
                Floor[validPosition.Item1, validPosition.Item2] = new Room(Room.RoomTypes.BossRoom, validPosition.Item1, validPosition.Item2, width, height, false);
                BossRoom = Floor[validPosition.Item1, validPosition.Item2];
                //validPositions.Clear();
            }
            else
            {
                Floor[validPosition.Item1, validPosition.Item2] = new Room(validPosition.Item1, validPosition.Item2, 1, 6, width, height, false); // rooms should be 1,6 but is 1,2 for testing
            }

            /// Set Room Prizes
            Floor[validPosition.Item1, validPosition.Item2].SetReward();

            // Add to filled rooms
            filledRooms.Add(validPosition);

            /// Remove validPosition from validPositions
            if (validPositions.Contains(validPosition))
            {
                validPositions.Remove(validPosition);
                //Debug.Log("(" + validPosition.Item1 + ", " + validPosition.Item2 + ") " + " removed");
            }

        }
    }

    private static List<(int, int)> addValidRooms(List<(int, int)> validPositions, List<(int, int)> filledRooms, (int, int) currentRoomPosition)
    {

        // North Room Check
        if (currentRoomPosition.Item1 - 1 >= 0 && 
            !filledRooms.Contains((currentRoomPosition.Item1  - 1, currentRoomPosition.Item2)) && 
            !validPositions.Contains((currentRoomPosition.Item1 - 1, currentRoomPosition.Item2)))
        {
            validPositions.Add((currentRoomPosition.Item1 - 1, currentRoomPosition.Item2));
        }

        // East Room Check
        if (currentRoomPosition.Item2 + 1 < FloorManager.MapRows && 
            !filledRooms.Contains((currentRoomPosition.Item1, currentRoomPosition.Item2 + 1)) &&
            !validPositions.Contains((currentRoomPosition.Item1, currentRoomPosition.Item2 + 1)))
        {
            validPositions.Add((currentRoomPosition.Item1, currentRoomPosition.Item2 + 2));
        }

        // South Room Check
        if (currentRoomPosition.Item1 + 1 < FloorManager.MapColumns && 
            !filledRooms.Contains((currentRoomPosition.Item1 + 1, currentRoomPosition.Item2)) &&
            !validPositions.Contains((currentRoomPosition.Item1 + 1, currentRoomPosition.Item2)))
        {
            validPositions.Add((currentRoomPosition.Item1 + 1, currentRoomPosition.Item2));
        }

        // West Room Check
        if (currentRoomPosition.Item2 - 1 >= 0 && 
            !filledRooms.Contains((currentRoomPosition.Item1, currentRoomPosition.Item2 - 1)) &&
            !validPositions.Contains((currentRoomPosition.Item1, currentRoomPosition.Item2 - 1)))
        {
            validPositions.Add((currentRoomPosition.Item1, currentRoomPosition.Item2 - 1));
        }

        return validPositions;
    }
    private void onLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name != "MainMenu")
        {
            CurrentRoom.OnRoomEnter();
            Debug.Log(CurrentRoom.Row + " " + CurrentRoom.Column + ": " + CurrentRoom.RoomName);
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
