using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject FloorManagerPrefab;
    public List<GameObject> PlayerCharactersPrefab; // how to find proper prefab...
    public enum GameStates
    {
        MainMenu,
        Game,
        Loading,
        GameOver,
    }

    public enum MenuStates
    {
        Pause
    }

    public GameStates State = GameStates.MainMenu;

    private void Awake()
    {
        // Intialize instance if null
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameState(GameStates newState)
    {
        State = newState;

        switch (State)
        {
            case GameStates.MainMenu:
                break;

            case GameStates.Game:
                break;

            case GameStates.Loading: // When player choses "Start Game" in Main Menu
                // Create Floor Manager
                GameObject FloorManagerObject = Instantiate(FloorManagerPrefab, new Vector3(0,0,0), Quaternion.identity);
                DontDestroyOnLoad(FloorManagerObject);

                //Generate Floor
                FloorManager.Instance.ResetFloor();

                // Load Floor
                //string roomName = Room.RoomTypes.GetName(typeof(Room.RoomTypes), FloorManager.StartingFloor.Type);
                //SceneManager.LoadScene(roomName, LoadSceneMode.Single);
                FloorManager.StartingFloor.IsCleared = true;
                FloorManager.CurrentRoom = FloorManager.StartingFloor;
                //Debug.Log("CurrentRoom assigned");

                // Spawn Player
                GameObject ChosenPlayerCharacter = Instantiate(PlayerCharactersPrefab[0], new Vector3(0, 0, 0), Quaternion.identity);
                DontDestroyOnLoad(ChosenPlayerCharacter);

                UpdateGameState(GameStates.Game);

                break;

            case GameStates.GameOver:
                break;

            default:
                break;

        }
    }
}
