using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private const int _roomSize = 2;
    public Room[,] Floor = new Room[_roomSize, _roomSize];

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

        // Fills Floor with Void Rooms
        for (int i = 0; i < Floor.GetLength(0); i++)
        {
            for (int j = 0; i < Floor.GetLength(1); j++)
            {
                var r = new Room(Room.RoomTypes.Void);
                Floor[i, j] = r;
            }
        }

        // Choose Start Room
    }
    private void createDoors()
    {

    }
        
       
}
