using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuControl : MonoBehaviour
{
    public Button Resume;
    public Button Restart;
    public Button Settings;
    public Button Quit;

    private void Update()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Resume = root.Q<Button>("Resume");
        Restart = root.Q<Button>("Restart");
        Settings = root.Q<Button>("Settings");
        Quit = root.Q<Button>("Quit");

        Resume.clicked += resumeButtonPressed;
        Restart.clicked += restartButtonPressed; 
        Settings.clicked += settingsButtonPressed;
        Quit.clicked += quitButtonPressed;
    }

    private void resumeButtonPressed()
    {
        if (GameManager.Instance.MenuState == GameManager.MenuStates.Pause)
        {
            unpause();
        }
    }

    private void restartButtonPressed()
    {
        if (GameManager.Instance.MenuState == GameManager.MenuStates.Pause)
        {
            unpause();
            GameManager.RestartGame(false);
        }
    }

    private void settingsButtonPressed()
    {
        if (GameManager.Instance.MenuState == GameManager.MenuStates.Pause)
        {
            GameManager.Instance.MenuState = GameManager.MenuStates.Settings;
        }
        Destroy(gameObject);
    }

    private void quitButtonPressed()
    {
        if (GameManager.Instance.MenuState == GameManager.MenuStates.Pause)
        {
            Application.Quit();
        }
    }
    
    private void unpause()
    {
        GameManager.Instance.MenuState = GameManager.MenuStates.None;

        Destroy(gameObject);
    }
}
