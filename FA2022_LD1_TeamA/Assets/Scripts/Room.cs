using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    public Scene SceneRoom { get; set; } // Might just have to be a string
    public enum RoomTypes // Make sure names are same as Scene names
    {
        EmptyRoom, 
        RegularRoom 
    }
    public RoomTypes Type { get; set; }
    public int Depth;
    public static int Height;
    public List<Room> ConnectedRooms;
    public int Choices = 2; // might be random later?
    // Door
    // Entites, Pickups, Powerups array
    public bool IsCleared = false;
    // array or list that holds number of entities in a room in the room

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
                Room r = new Room(Room.RoomTypes.EmptyRoom); // testing with only empty rooms
                ConnectedRooms.Add(r);
                r.GenerateRooms(depth++);
            }
        } else if (depth == Height)
        {
            //ConnectedRooms.Add(Boss room);
        }
    }

    public void OnRoomEnter() 
    {
        // Check if ...
            // room is full of enemies
                // if true, room is cleared
            //room
    }

    private void spawnRoomItems()
    {
        // spawns entites, powerups, pickups, doors, etc
    }
}
