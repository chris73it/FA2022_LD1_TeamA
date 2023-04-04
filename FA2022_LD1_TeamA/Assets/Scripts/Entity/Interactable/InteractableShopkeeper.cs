using UnityEngine;

public class InteractableShopkeeper : Interactable
{
    public enum Relationship
    {
        FirstEncounter,
        Prepurchase,
        Purchased,
    }

    public Relationship CurrentRelationship = Relationship.FirstEncounter;

    public override void Interact()
    {
        base.Interact();

        switch (CurrentRelationship)
        {
            case Relationship.FirstEncounter:
                DialogueManager.Instance.CreateDialogueBox(DialogueManager.Scenarios.ShopkeeperFirstEncounter);
                CurrentRelationship = Relationship.Prepurchase;
                break;

            case Relationship.Prepurchase:
                switch (Random.Range(1, 3))
                {
                    case 1:
                        DialogueManager.Instance.CreateDialogueBox(DialogueManager.Scenarios.ShopkeeperPrepurchase1);
                        break;

                    case 2:
                        DialogueManager.Instance.CreateDialogueBox(DialogueManager.Scenarios.ShopkeeperPrepurchase2);
                        break;
                    default:
                        break;
                }
                break;

            case Relationship.Purchased:
                switch (Random.Range(1, 3))
                {
                    case 1:
                        DialogueManager.Instance.CreateDialogueBox(DialogueManager.Scenarios.ShopkeeperPurchased1);
                        break;

                    case 2:
                        DialogueManager.Instance.CreateDialogueBox(DialogueManager.Scenarios.ShopkeeperPurchased2);
                        break;
                    default:
                        break;
                }
                break;

            default: 
                break;
        }
    }
}
