using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool Colliding = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Colliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Colliding = false;
        }
    }

    private void Update()
    {
        if (Colliding && Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        Debug.Log("test");
    }
}
