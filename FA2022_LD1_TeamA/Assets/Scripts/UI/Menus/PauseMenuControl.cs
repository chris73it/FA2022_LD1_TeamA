using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuControl : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            unpause();
        }
    }

    private void unpause()
    {
        GameManager.Instance.MenuState = GameManager.MenuStates.None;

        Destroy(gameObject);
    }
}
