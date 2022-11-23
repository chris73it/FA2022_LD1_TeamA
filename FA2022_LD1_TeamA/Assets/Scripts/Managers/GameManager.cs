using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject FloorManagerPrefab;
    public GameObject PlayerUIPrefab;
    public List<GameObject> PlayerCharactersPrefab; // how to find proper prefab...
    public List<GameObject> Menus;
    public GameObject TextObject;
    public static GameObject ChosenPlayerCharacter;
    public enum GameStates
    {
        Menu,
        Game,
        Loading,
        GameOver,
    }

    public enum MenuStates
    {
        None,
        Main,
        GameOver,
        Pause
    }
    private MenuStates menuState = MenuStates.Main;
    public MenuStates MenuState
    {
        get
        {
            return menuState;
        }

        set
        {
            var previousState = menuState;
            menuState = value;
            switch (menuState)
            {
                case (MenuStates.None):
                    Time.timeScale = 1;
                    break;

                case (MenuStates.Main):
                    Instantiate(Menus[0]);
                    Time.timeScale = 0;
                    break;

                case (MenuStates.GameOver):
                    Instantiate(Menus[1]);
                    Time.timeScale = 0;
                    break;

                case (MenuStates.Pause):
                    Instantiate(Menus[2]);
                    Time.timeScale = 0;
                    break;

                default:
                    break;
            }
        }
    }

    public bool NextFloor = true;

    private GameStates gameState = GameStates.Menu;
    public GameStates GameState
    {
        get
        {
            return gameState;
        }
        set
        {
            gameState = value;

            switch (GameState)
            {
                case GameStates.Menu:              
                    Destroy(PlayerUIControl.Instance.gameObject); // Destroying all of these should be in a separate method
                    Destroy(gameObject);
                    SceneManager.LoadScene("MainMenu");
                    break;

                case GameStates.Game:
                    MenuState = MenuStates.None;
                    NextFloor = true;
                    break;

                case GameStates.Loading:
                    // Create Floor Manager
                    Debug.Log("Loading");
                    if (FloorManager.Instance == null)
                    {
                        GameObject FloorManagerObject = Instantiate(FloorManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        DontDestroyOnLoad(FloorManagerObject);
                        Debug.Log("Check");
                    }

                    //Generate Floor
                    FloorManager.Instance.ResetFloor(false); // false should be replaced by a variable that is properly set after death or clearing a floor

                    // Load Floor
                    //string roomName = Room.RoomTypes.GetName(typeof(Room.RoomTypes), FloorManager.StartingFloor.Type);
                    //SceneManager.LoadScene(roomName, LoadSceneMode.Single);
                    FloorManager.StartingFloor.IsCleared = true; // Redundant
                    FloorManager.CurrentRoom = FloorManager.StartingFloor;
                    //Debug.Log("CurrentRoom assigned");

                    // Spawn Player
                    if (ChosenPlayerCharacter != null)
                    {
                        if (!NextFloor)
                        {
                            Destroy(ChosenPlayerCharacter);
                        }
                    } else
                    {
                        ChosenPlayerCharacter = Instantiate(PlayerCharactersPrefab[0], new Vector3(0, 0, 0), Quaternion.identity);
                        DontDestroyOnLoad(ChosenPlayerCharacter);
                    }


                    if (PlayerUIControl.Instance == null)
                    {
                        GameObject PlayerUIObject = Instantiate(PlayerUIPrefab);
                        DontDestroyOnLoad(PlayerUIObject);
                    }

                    PlayerUIControl.Instance.InitializeHealth();
                    PlayerUIControl.Instance.UpdateStamina(ChosenPlayerCharacter.GetComponent<PlayerMovement>().CurrentStamina,
                        ChosenPlayerCharacter.GetComponent<PlayerMovement>().MaxStamina);

                    GameState = GameStates.Game;
                    break;

                case GameStates.GameOver:
                    MenuState = MenuStates.GameOver;
                    break;

                default:
                    break;

            }
        }
    }

    private void Awake()
    {
        // Intialize instance if null
        if (Instance == null)
        {
            Instance = this;
        }

        Application.targetFrameRate = 60;

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        MenuState = MenuStates.Main;
    }

    public static void RestartGame(bool check)
    {
        GameManager.Instance.NextFloor = check;
        GameManager.Instance.GameState = GameManager.GameStates.Loading;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameState == GameStates.Game && MenuState == MenuStates.None)
            {
                MenuState = MenuStates.Pause;
            }
        }
    }
}
