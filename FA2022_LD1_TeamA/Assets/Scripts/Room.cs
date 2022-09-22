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
        Basic 
    }
    public RoomTypes Type { get; set; }

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
    private Scene getRandomScene()
    {
        var randomScene = Random.Range(0, SceneManager.sceneCountInBuildSettings - 1);
        return SceneManager.GetSceneByBuildIndex(randomScene);
    }

    private RoomTypes getRandomType()
    {
        var roomTypesLength = RoomTypes.GetNames(typeof(RoomTypes)).Length;
        return (RoomTypes)Random.Range(0, roomTypesLength); // might have to cast to int first?
    }
}
