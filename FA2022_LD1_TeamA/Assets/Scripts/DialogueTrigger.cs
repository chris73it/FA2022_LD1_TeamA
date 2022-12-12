using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject DialogBox;
    public GameObject Manager;

    private void Awake()
    {
        dialogue = new Dialogue();
        dialogue.name = "Wolf";

        switch (FloorManager.FloorsCompleted)
        {
            case 0:
                dialogue.sentences = new string[2];
                dialogue.sentences[0] = "Use WASD to move. Left click to attack. Right click to dash.";
                dialogue.sentences[1] = "Now. It's time to get out of the woods.";
                dialogue.sentences[1] = "I will attack anything that gets in my way!";
                break;

            case 1:
                break;

            case 2:
                break;

            default:
                break;
        }
    }

    public void TriggerDialogue()
    {
        Instantiate(DialogBox);
        Instantiate(Manager).GetComponent<DialogueManager>().StartDialogue(dialogue);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (FloorManager.FloorsCompleted == 0) {
            TriggerDialogue();
            Destroy(gameObject);
        }

        Debug.Log("BRUH");
    }

}
