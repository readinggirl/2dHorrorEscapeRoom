using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Chest : MonoBehaviour, IInteractable
{
    private bool IsOpened { get; set; }

    public GameObject itemPrefab; //key
    
    public GameObject uiChestCode;
    public TMP_InputField passwordInput;
    [SerializeField] private string password = "HOLY";

    public TMP_Text reactionText;
    
    public AudioClip openChestClip;

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
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        //show UI
        if (!uiChestCode.activeInHierarchy)
        {
            //play sound
            SoundFXManager.instance.PlaySoundFXClip(openChestClip, transform, 1f);
            
            Time.timeScale = 0;
            uiChestCode.SetActive(true);
            passwordInput.text = "";
            passwordInput.ActivateInputField();
            
        }
    }

    public void CheckPassword()
    {
        if (string.Equals(passwordInput.text.Trim(), password.Trim(), StringComparison.CurrentCultureIgnoreCase))
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
            //key sparkle sound
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
            Debug.Log(droppedItem + "instantiated");
        }

        Time.timeScale = 1;
    }

    private void SetOpened(bool opened)
    {
        if (IsOpened = opened)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
}