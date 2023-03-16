using UnityEngine;
using UnityEngine.UIElements;

public class DialogueUIControl : MonoBehaviour
{
    public VisualElement Root;
    public VisualElement Container;
    private Button continueButton;
    private Label nameText;
    private Label dialogueText;

    private void Awake()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        Container = Root.Q<VisualElement>("Container");
        continueButton = Root.Q<VisualElement>("DialogueBox").Q<Button>("Continue");
        nameText = Root.Q<VisualElement>("DialogueBox").Q<Label>("Name");
        dialogueText = Root.Q<VisualElement>("DialogueBox").Q<Label>("DialogueText");

        continueButton.clicked += continuePressed;

        Container.RegisterCallback<TransitionStartEvent>(rootTranstionStart);
    }


    private void rootTranstionStart(TransitionStartEvent evt)
    {
        Debug.Log("test");
    }

    private void Start()
    {
        Container.style.top = new StyleLength(new Length(0, LengthUnit.Percent));
    }

    private void continuePressed()
    {

        if (DialogueManager.Instance.CurrentIndex + 1 < DialogueManager.Instance.Dialogue.Count)
        {
            DialogueManager.Instance.CurrentIndex++;

            UpdateDialogueBox();
        } else
        {
            Destroy(this.gameObject);
            Time.timeScale = 1;
        }
    }

    public void UpdateDialogueBox()
    {

        nameText.text = DialogueManager.Instance.Dialogue[DialogueManager.Instance.CurrentIndex].Item1;
        dialogueText.text = DialogueManager.Instance.Dialogue[DialogueManager.Instance.CurrentIndex].Item2;
    }

}
