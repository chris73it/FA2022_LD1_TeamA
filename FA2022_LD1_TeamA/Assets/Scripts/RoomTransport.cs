using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransport : MonoBehaviour
{
    public Room NextRoom; // Should be set on creation of door which is after floor manager and its room have been created
    public bool NextFloor = false;
    public int Direction = -1;
    public bool Active = false;

    private void Awake()
    {
        if (!NextFloor)
        {
            SetRoom(Direction);

            if (FloorManager.CurrentRoom.IsCleared)
            {
                Active = true;
            }
        } else
        {
            if (!FloorManager.CurrentRoom.DoorsSpawned.Contains(gameObject))
            {
                FloorManager.CurrentRoom.DoorsSpawned.Add(gameObject);
            }

            transform.gameObject.SetActive(false);
        }
    }

    // Transports player to nextRoom
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Active)
        {

            if (!NextFloor)
            {
                FloorManager.LastDoorDirection = Direction;
                FloorManager.CurrentRoom = NextRoom;
            }
            else
            {
                if (FloorManager.CurrentRoom.IsCleared)
                {
                    if (FloorManager.FloorsCompleted >= GameManager.GameWinCondition)
                    {
                        GameManager.Instance.WinGame();
                    }
                    else
                    {
                        GameManager.RestartGame(true);
                    }
                }
            }
        }
    }

    // Sets NextRoom to proper location if it exists
    public void SetRoom(int direction)
    {
        Direction = direction;
        switch (Direction)
        {
            // North
            case 0:
                //Debug.Log("Case 0");

                if (FloorManager.CurrentRoom.Row - 1 >= 0)
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row - 1, FloorManager.CurrentRoom.Column];
                }

                break;

            case 1:
                //Debug.Log("Case 1");

                if (FloorManager.CurrentRoom.Row - 1 >= 0)
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row - 1, FloorManager.CurrentRoom.Column];
                }

                break;

            // East
            case 2:
                //Debug.Log("Case 2");

                if (FloorManager.CurrentRoom.Column + 1 < FloorManager.Instance.Floor.GetLength(1))
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row, FloorManager.CurrentRoom.Column + 1];
                }

                break;

            case 3:
                //Debug.Log("Case 3");

                if (FloorManager.CurrentRoom.Column + 1 < FloorManager.Instance.Floor.GetLength(1))
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row, FloorManager.CurrentRoom.Column + 1];
                }

                break;

            // South
            case 4:
                //Debug.Log("Case 4");

                if (FloorManager.CurrentRoom.Row + 1 < FloorManager.Instance.Floor.GetLength(0))
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row + 1, FloorManager.CurrentRoom.Column];
                }

                break;

            case 5:
                //Debug.Log("Case 5");

                if (FloorManager.CurrentRoom.Row + 1 < FloorManager.Instance.Floor.GetLength(0))
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row + 1, FloorManager.CurrentRoom.Column];
                }

                break;

            // West
            case 6:
                //Debug.Log("Case 6");

                if (FloorManager.CurrentRoom.Column - 1 >= 0)
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row, FloorManager.CurrentRoom.Column - 1];
                }
                break;

            case 7:
                //Debug.Log("Case 7");

                if (FloorManager.CurrentRoom.Column - 1 >= 0)
                {
                    NextRoom = FloorManager.Instance.Floor[FloorManager.CurrentRoom.Row, FloorManager.CurrentRoom.Column - 1];
                }
                break;

            default:
                break;
        }

        if (NextRoom == null) // || (NextRoom.Type == Room.RoomTypes.ShopRoom && FloorManager.CurrentRoom.Type != Room.RoomTypes.BossRoom)
        {
            //Debug.Log("NextRoom is null");
            Destroy(gameObject);
        } else
        {
            if (!FloorManager.CurrentRoom.DoorsSpawned.Contains(gameObject))
            {
                FloorManager.CurrentRoom.DoorsSpawned.Add(gameObject);
            }
        }
    }
}

// if nextroom == shop and current room is bossroom, then add
// if nextroom != shop and current room != bossroom