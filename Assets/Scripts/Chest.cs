using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour, IInteractable
{
    private bool isOpened { get; set; }

    public GameObject itemPrefab; //key
    
    public GameObject uiChestCode;
    public TMP_InputField passwordInput;
    public const string Password = "HOLY";

    public TMP_Text reactionText;

    private void Update()
    {
        if (uiChestCode.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            uiChestCode.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public bool CanInteract()
    {
        return !isOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        //show UI
        if (!uiChestCode.activeInHierarchy)
        {
            Time.timeScale = 0;
            uiChestCode.SetActive(true);
            passwordInput.text = "";
            passwordInput.ActivateInputField();
        }
    }

    public void CheckPassword()
    {
        if (string.Equals(passwordInput.text.Trim(), Password.Trim(), StringComparison.CurrentCultureIgnoreCase))
        {
            Debug.Log("Correct Code! Key spawned.");
            uiChestCode.SetActive(false);
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
            Debug.Log(droppedItem + "instantiated");
        }

        Time.timeScale = 1;
    }

    private void SetOpened(bool opened)
    {
        if (isOpened = opened)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
}