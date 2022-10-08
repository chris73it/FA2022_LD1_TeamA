using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class MainMenuControl : MonoBehaviour
{
    public Button StartGame;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        StartGame = root.Q<Button>("Start-Game");

        StartGame.clicked += StartGamePressed;
    }

    private void StartGamePressed()
    {
        //Debug.Log(GameManager.Instance);
        GameManager.Instance.UpdateGameState(GameManager.GameStates.Loading);
    }
}
