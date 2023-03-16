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
