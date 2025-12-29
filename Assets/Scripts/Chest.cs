using Unity.Mathematics;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; } = false;

    public GameObject itemPrefab; //key

    public Sprite openedSprite;

    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        OpenChest();
    }

    private void OpenChest()
    {
        SetOpened(true);

        //drop key
        if (itemPrefab)
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
            Debug.Log(droppedItem + "instatiated");
            
        }
    }

    public void SetOpened(bool opened)
    {
        if (IsOpened = opened)
        {
           
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }
}