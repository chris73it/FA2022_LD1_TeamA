using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (FloorManager.CurrentRoom.Type == Room.RoomTypes.StartingRoom && FloorManager.FloorsCompleted != 0)
        {
            Destroy(gameObject);
        }
    }

  
}
