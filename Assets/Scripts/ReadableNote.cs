using UnityEngine;

public class ReadableNote : MonoBehaviour, IInteractable
{
    [TextArea(3, 10)] public string noteText;

    [SerializeField] private NoteUI displayUI;

    public void Interact()
    {
        if (displayUI.isOpen)
        {
            displayUI.CloseNote();
        }
        else
        {
            displayUI.OpenNote(noteText);
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}