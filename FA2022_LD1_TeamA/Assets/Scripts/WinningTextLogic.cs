using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningTextLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public float Timer = 0f;
    public float Duration = 7f;

    // Update is called once per frame
    void Update()
    {
        if (Timer < Duration)
        {
            Timer += Time.deltaTime;
            transform.position = new Vector3(Camera.main.transform.position.x, 11, Camera.main.transform.position.z);
        }
        else
        {
            Destroy(gameObject);
            GameManager.Instance.GameState = GameManager.GameStates.Menu;
            GameManager.Instance.GameWon = false;
        }
    }
}
