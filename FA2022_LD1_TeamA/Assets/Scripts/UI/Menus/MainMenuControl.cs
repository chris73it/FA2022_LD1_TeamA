using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class MainMenuControl : MonoBehaviour
{
    public Button StartGame;
    public Button ExitGame;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        StartGame = root.Q<Button>("StartGame");

        StartGame.clicked += StartGamePressed;
        ExitGame.clicked += QuitGamePressed;
    }

    private void StartGamePressed()
    {
        //Debug.Log(GameManager.Instance);
        if (GameManager.Instance.MenuState == GameManager.MenuStates.Main)
        {
            GameManager.Instance.GameState = GameManager.GameStates.Loading;
        }
    }

    private void QuitGamePressed()
    {
        Application.Quit();
    }
}
