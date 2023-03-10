using UnityEngine;
using UnityEngine.UIElements;

public class DialogueUIControl : MonoBehaviour
{
    private VisualElement root;
    private Button continueButton;
    private Label nameText;
    private Label dialogueText;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        continueButton = root.Q<VisualElement>("DialogueBox").Q<Button>("Continue");
        nameText = root.Q<VisualElement>("DialogueBox").Q<Label>("Name");
        dialogueText = root.Q<VisualElement>("DialogueBox").Q<Label>("DialogueText");

        continueButton.clicked += continuePressed;
    }

    private void continuePressed()
    {
        DialogueManager.Instance.CurrentIndex++;

        nameText.text = DialogueManager.Instance.Dialogue[DialogueManager.Instance.CurrentIndex].Item1;
        dialogueText.text = DialogueManager.Instance.Dialogue[DialogueManager.Instance.CurrentIndex].Item2;
    }

}
