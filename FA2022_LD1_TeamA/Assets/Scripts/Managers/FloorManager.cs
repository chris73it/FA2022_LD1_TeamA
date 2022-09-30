using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance = null;

    // Floor Attributes
    public enum FloorTypes
    {
        Base,
        Forest
    }

    public FloorTypes Type = FloorTypes.Base;
    public static int RoomTreeHeight = 3;
    public Room StartingFloor;

    // Init:
    // Check if instance exists already, if not set instance = this;
    // Determine Floor Type, FloorSize
    // Flood Floor with Rooms with Type == RoomsType.Nothing
    // Choose one random room (as a start, make sure it is an empty default room)
    // Generate other rooms based off that
    private void Awake()
    {
        // Intialize instance if null
        if (Instance != null)
        {
            Instance = this;
        }

        ResetFloor();
    }

    // Methods
    public void ResetFloor()
    {
        // Switch to next Floor Type
        Type++; // ?

        // Generate Starting Room
        StartingFloor = new Room(SceneManager.GetSceneByName("EmptyRoom"), Room.RoomTypes.Empty);
        // if room instanstiation doesnt work just move the Room constructor to an private Awake

        // Generate Starting Room Connections

    }
    private void createDoors()
    {

    }
      
      // generate starting room
      // generate its connections
      // repeat until for loop reaches height,
      // then make all last rooms connect to boss
}
