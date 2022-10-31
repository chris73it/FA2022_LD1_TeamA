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
    public MenuStates MenuState = MenuStates.Main;

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
                    Time.timeScale = 0;
                    Destroy(PlayerUIControl.Instance.gameObject); // Destroying all of these should be in a separate method
                    Destroy(gameObject);
                    SceneManager.LoadScene("MainMenu");

                    break;

                case GameStates.Game:
                    MenuState = MenuStates.None;
                    Time.timeScale = 1;
                    break;

                case GameStates.Loading:
                    // Create Floor Manager
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
                    ChosenPlayerCharacter = Instantiate(PlayerCharactersPrefab[0], new Vector3(0, 0, 0), Quaternion.identity);
                    DontDestroyOnLoad(ChosenPlayerCharacter);

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
                    Instantiate(Menus[1]); // maybe move menu instantiating to switch menustate case
                    MenuState = MenuStates.GameOver;
                    Time.timeScale = 0;
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
