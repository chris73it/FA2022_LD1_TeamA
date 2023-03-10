using UnityEngine;
using System.Collections.Generic;

public class DialogueManager
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

            Dialogue.Clear();

            switch (currentScenario)
            {
                case Scenarios.Tutorial:
                    Dialogue.Add(("Wolf", "RAAAAH IM SO MAD"));
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


    public DialogueManager()
    {
        // Intialize instance if null
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreateDialogueBox(Scenarios scenario)
    {
        currentScenario = scenario;
        CurrentIndex = 0;
        DialogueTextboxObject = GameObject.Instantiate(DialogueTextboxPrefab);

    }
}
