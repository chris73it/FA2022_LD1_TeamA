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
        if (GameManager.Instance.MenuState == GameManager.MenuStates.GameOver)
        {
            GameManager.Instance.GameState = GameManager.GameStates.Loading;
            Destroy(gameObject);
        } 
    }
    private void MainMenuPressed()
    {
        if (GameManager.Instance.MenuState == GameManager.MenuStates.GameOver)
        {
            //Debug.Log(GameManager.Instance);
            GameManager.Instance.GameState = GameManager.GameStates.Menu;
            Destroy(gameObject);
        }
        
    }
    private void QuitPressed()
    {
        if (GameManager.Instance.MenuState == GameManager.MenuStates.GameOver)
        {
            //Debug.Log(GameManager.Instance)
            Application.Quit(); // Only works in a build
        }
    }
}
