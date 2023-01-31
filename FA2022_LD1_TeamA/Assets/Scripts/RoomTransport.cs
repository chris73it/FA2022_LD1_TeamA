using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransport : MonoBehaviour
{
    public Room NextRoom; // Should be set on creation of door which is after floor manager and its room have been created
    public bool NextFloor = false;
    public int Direction = -1;

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
    public void SetRoom(int direction)
    {
        Direction = direction;
        switch (Direction)
        {
            // North
            case 0:
                Debug.Log("Case 0");

                if (FloorManager.CurrentRoom.Row - 1 >= 0)
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row - 1, FloorManager.CurrentRoom.Column];
                }

                break;

            case 1:
                Debug.Log("Case 1");

                if (FloorManager.CurrentRoom.Row - 1 >= 0)
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row - 1, FloorManager.CurrentRoom.Column];
                }

                break;

            // East
            case 2:
                Debug.Log("Case 2");

                if (FloorManager.CurrentRoom.Column + 1 < FloorManager.Instance.Floor.GetLength(1))
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row, FloorManager.CurrentRoom.Column + 1];
                }

                break;

            case 3:
                Debug.Log("Case 3");

                if (FloorManager.CurrentRoom.Column + 1 < FloorManager.Instance.Floor.GetLength(1))
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row, FloorManager.CurrentRoom.Column + 1];
                }

                break;

            // South
            case 4:
                Debug.Log("Case 4");

                if (FloorManager.CurrentRoom.Row + 1 < FloorManager.Instance.Floor.GetLength(0))
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row + 1, FloorManager.CurrentRoom.Column];
                }

                break;

            case 5:
                Debug.Log("Case 5");

                if (FloorManager.CurrentRoom.Row + 1 < FloorManager.Instance.Floor.GetLength(0))
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row + 1, FloorManager.CurrentRoom.Column];
                }

                break;

            // West
            case 6:
                Debug.Log("Case 6");

                if (FloorManager.CurrentRoom.Column - 1 >= 0)
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row, FloorManager.CurrentRoom.Column - 1];
                }
                break;

            case 7:
                Debug.Log("Case 7");

                if (FloorManager.CurrentRoom.Column - 1 >= 0)
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row, FloorManager.CurrentRoom.Column - 1];
                }
                break; 

            default:
                break;
        }

        if (NextRoom == null)
        {
            Debug.Log("NextRoom is null");
            Destroy(gameObject);
        }
    }
}
