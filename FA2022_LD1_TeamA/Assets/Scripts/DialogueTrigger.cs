using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void Awake()
    {
        switch (FloorManager.FloorsCompleted)
        {
            case 0:
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
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TriggerDialogue();
    }
}
