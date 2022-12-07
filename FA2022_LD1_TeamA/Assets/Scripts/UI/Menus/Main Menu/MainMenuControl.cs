using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class MainMenuControl : MonoBehaviour
{
    public Button StartGame;
    public Button Settings;
    public Button ExitGame;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        StartGame = root.Q<Button>("StartGame");
        Settings = root.Q<Button>("Settings");
        ExitGame = root.Q<Button>("ExitGame");

        StartGame.clicked += StartGamePressed;
        Settings.clicked += SettingsPressed;
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

    private void SettingsPressed()
    {
        if (GameManager.Instance.MenuState == GameManager.MenuStates.Main)
        {
            GameManager.Instance.MenuState = GameManager.MenuStates.Settings;
        }

        Destroy(gameObject);
    }

    private void QuitGamePressed()
    {
        Application.Quit();
    }
}
