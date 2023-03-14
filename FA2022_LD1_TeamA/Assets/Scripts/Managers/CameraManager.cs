using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    public static CameraManager Instance = null;

    public CameraManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CameraUpdate()
    {
        if (GameManager.ChosenPlayerCharacter != null)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(GameManager.ChosenPlayerCharacter.transform.position.x,
                        12,
                        GameManager.ChosenPlayerCharacter.transform.position.z), 5 * Time.deltaTime);
        }
    }
}
