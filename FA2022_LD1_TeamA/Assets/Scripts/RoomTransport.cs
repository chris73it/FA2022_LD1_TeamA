using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransport : MonoBehaviour
{
    private Room nextRoom; // Should be set on creation of door which is after floor manager and its room have been created

    // Method: OnCollisionEnter, transports player to nextRoom
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.MoveGameObjectToScene(collision.gameObject, nextRoom.SceneRoom);
        }
    }
}
