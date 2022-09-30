using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    public Scene SceneRoom { get; set; }
    public enum RoomTypes
    {
        Void, // Inaccessible rooms
        Empty,
        Regular 
    }
    public RoomTypes Type { get; set; }
    public int Depth;
    public static int Height;
    public List<Room> ConnectedRooms;
    public int Choices = 2;
    // Door
    // Entites, Pickups, Powerups array
    private bool _isCleared = false;
    private bool _hasEntered = false;
    // array or list that holds number of entities in a room in the room

    // Constructors
    public Room()
    {
        SceneRoom = getRandomScene();
        Type = getRandomType();
    }

    public Room(Scene s)
    {
        SceneRoom = s;
        Type = getRandomType();
    }

    public Room(RoomTypes t)
    {
        SceneRoom = getRandomScene();
        Type = t;
    }

    public Room(Scene s, RoomTypes t)
    {
        SceneRoom = s;
        Type = t;
    }

    // Methods
    private static Scene getRandomScene()
    {
        var randomScene = Random.Range(0, SceneManager.sceneCountInBuildSettings - 1);
        return SceneManager.GetSceneByBuildIndex(randomScene);
    }

    private static RoomTypes getRandomType()
    {
        var roomTypesLength = RoomTypes.GetNames(typeof(RoomTypes)).Length;
        return (RoomTypes)Random.Range(0, roomTypesLength); // might have to cast to int first?
    }

    public void GenerateRooms(int depth)
    {
        Depth = depth;

        if (depth < Height)
        {
            for (int i = 0; i < Choices; i++)
            {
                Room r = new Room(SceneManager.GetSceneByName("EmptyRoom"), Room.RoomTypes.Empty); // testing with only empty rooms
                ConnectedRooms.Add(r);
                r.GenerateRooms(depth++);
            }
        } else if (depth == Height)
        {
            //ConnectedRooms.Add(Boss room);
        }
    }

    /* Fill Rooms
     * Go to one
     * Repeat until hit depth - 1 or array is full
     * 
    */
}
