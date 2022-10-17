using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class GameOverControl : MonoBehaviour
{
    public Button Restart;
    public Button MainMenu;
    public Button Quit;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Restart = root.Q<Button>("Restart");
        MainMenu = root.Q<Button>("MainMenu");
        Quit = root.Q<Button>("Quit");

        Restart.clicked += RestartPressed;
        MainMenu.clicked += MainMenuPressed;
        Quit.clicked += QuitPressed;
    }

    private void RestartPressed()
    {
        //Debug.Log(GameManager.Instance);
        GameManager.Instance.GameState = GameManager.GameStates.Loading;
        Destroy(gameObject);
    }
    private void MainMenuPressed()
    {
        //Debug.Log(GameManager.Instance);
        GameManager.Instance.GameState = GameManager.GameStates.Loading;
        Destroy(gameObject);
    }
    private void QuitPressed()
    {
        //Debug.Log(GameManager.Instance)
        Application.Quit(); // Only works in a build
    }
}
