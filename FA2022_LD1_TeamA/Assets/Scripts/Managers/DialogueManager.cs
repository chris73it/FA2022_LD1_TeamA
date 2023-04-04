using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    // Reference to Self
    public static DialogueManager Instance;

    // Dialogue Textbox Prefab
    public GameObject DialogueTextboxPrefab;

    // Dialogue Textbox Object
    public GameObject DialogueTextboxObject;

    // Determines which dialogue to load
    public enum Scenarios
    {
        None,
        Tutorial,
        Beginning,
        ShopkeeperFirstEncounter,
        ShopkeeperPrepurchase1,
        ShopkeeperPrepurchase2,
        ShopkeeperPurchased1,
        ShopkeeperPurchased2,
    }

    private Scenarios currentScenario = Scenarios.None;
    public Scenarios CurrentScenario { 
        get 
        { 
            return currentScenario; 
        }
        
        set
        {
            currentScenario = value;

            // Debug.Log("Dialogue Box");

            Dialogue.Clear();

            switch (currentScenario)
            {
                case Scenarios.Tutorial:
                    Dialogue.Add(("Wolf", "RAAAAH IM SO MAD"));
                    Dialogue.Add(("Test1", "Test1"));
                    Dialogue.Add(("Test2", "Test2"));
                    Dialogue.Add(("Test3", "Test3"));
                    Dialogue.Add(("Test4", "Test4"));
                    break;

                case Scenarios.ShopkeeperFirstEncounter:
                    Dialogue.Add(("Wolf", "I wasn't expecting anyone left in the village. Who are you, child?"));
                    Dialogue.Add(("Shopkeeper", "I-I'm the daughter of the shopkeeper who ran this place... Before everyone went mad."));
                    Dialogue.Add(("Shopkeeper", "Please... Don't hurt me, I mean you no harm."));
                    Dialogue.Add(("Wolf", "Clearly. You couldn't hurt a fly. I'll leave you be then."));
                    Dialogue.Add(("Shopkeeper", "Wait! B-before you go... I could help you."));
                    Dialogue.Add(("Shopkeeper", "I have items that could aid as you fight. If you have any spare coin, I wouldn't mind selling them..."));
                    Dialogue.Add(("Wolf", "Tch. Fine... I'll give you a shot, child. Show me your wares."));

                    break;

                case Scenarios.ShopkeeperPrepurchase1:
                    Dialogue.Add(("Wolf", "What do you have for sale this time, child?"));
                    Dialogue.Add(("Shopkeeper", "T-take a look..."));
                    break;

                case Scenarios.ShopkeeperPrepurchase2:
                    Dialogue.Add(("Wolf", "Child! You know the drill."));
                    Dialogue.Add(("Shopkeeper", "Y-yeah..."));
                    break;

                case Scenarios.ShopkeeperPurchased1:
                    Dialogue.Add(("Wolf", "Hmph... Adequate."));
                    Dialogue.Add(("Shopkeeper", "Please... use it to free them."));
                    break;

                case Scenarios.ShopkeeperPurchased2:
                    Dialogue.Add(("Wolf", "ready to go now."));
                    Dialogue.Add(("Shopkeeper", "You... better come back safe."));
                    break;
                default:
                    Dialogue.Add(("Programmer", "How did you get here?"));
                    break;
            }
        }
    }

    // Dialogue in current Scenario
    public List<(string, string)> Dialogue = new List<(string, string)>();

    // Current Dialogue Text to display
    public int CurrentIndex = 0;


    private void Awake()
    {
        // Intialize instance if null
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreateDialogueBox(Scenarios scenario)
    {
        CurrentScenario = scenario;
        CurrentIndex = 0;

        if (DialogueTextboxObject == null)
        {
            DialogueTextboxObject = GameObject.Instantiate(DialogueTextboxPrefab);
            DontDestroyOnLoad(DialogueTextboxObject);
        }

        DialogueTextboxObject.GetComponent<DialogueUIControl>().UpdateDialogueBox();

        Time.timeScale = 0;
    }
}
