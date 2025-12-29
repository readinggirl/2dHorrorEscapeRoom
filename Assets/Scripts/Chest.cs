using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; } = false;

    public GameObject itemPrefab; //key

    public Sprite openedSprite;

    public GameObject UIChestCode;
    public TMP_InputField passwordInput;
    private readonly string password = "HOLY";

    public TMP_Text reactionText;

    private void Update()
    {
        if (UIChestCode.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UIChestCode.SetActive(false);
        }
    }

    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        //show UI
        if (!UIChestCode.activeInHierarchy)
        {
            UIChestCode.SetActive(true);
            passwordInput.text = "";
            passwordInput.ActivateInputField();
        }
    }

    public void CheckPassword()
    {
        if (passwordInput.text.Trim().ToUpper() == password) 
        {
            Debug.Log("Correct Code! Key spawned.");
            UIChestCode.SetActive(false);
            OpenChest();
        }
        else 
        {
            Debug.Log("Wrong Code.");
            Debug.Log(passwordInput.text);
            reactionText.text = "Wrong Code!";
        }
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