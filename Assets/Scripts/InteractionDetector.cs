using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable _interactableInRange;
    public GameObject interactionIcon;
    public TMPro.TMP_Text interactionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactableInRange?.Interact(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            _interactableInRange = interactable;
            interactionIcon.SetActive(true);
            if(interactionText != null) interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable == _interactableInRange)
        {
            _interactableInRange = null;
            interactionIcon.SetActive(false);
            if(interactionText != null) interactionText.gameObject.SetActive(false);
            
        }
    }
}