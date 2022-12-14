using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransport : MonoBehaviour
{
    public Room NextRoom; // Should be set on creation of door which is after floor manager and its room have been created
    public bool NextFloor = false;

    // Method: OnCollisionEnter, transports player to nextRoom
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //SceneManager.MoveGameObjectToScene(collision.gameObject, nextRoom.SceneRoom);
            //string roomName = Room.RoomTypes.GetName(typeof(Room.RoomTypes), NextRoom.Type);
            //GameObject p = collision.gameObject;
            //DontDestroyOnLoad(p); // move dont destroy on load stuff to game manager
            //Debug.Log(p.ToString());
            //SceneManager.LoadScene(roomName, LoadSceneMode.Single); 

            //Debug.Log(NextRoom.RoomName);
           
            if (!NextFloor)
            {
                FloorManager.CurrentRoom = NextRoom;
            } else
            {
                if (FloorManager.FloorsCompleted >= GameManager.GameWinCondition)
                {
                    GameManager.Instance.WinGame();
                } else
                {
                    GameManager.RestartGame(true);
                }
            }
        }
    }
}
