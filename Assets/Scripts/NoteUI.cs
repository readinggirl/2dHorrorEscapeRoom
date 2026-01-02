using TMPro;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI textElement;
    public bool isOpen { get; private set; }

    void Start()
    {
        panel.SetActive(false);
    }

    public void OpenNote(string content)
    {
        textElement.gameObject.SetActive(true);
        textElement.text = content;
        panel.SetActive(true);
        isOpen = true;
        Time.timeScale = 0f;
    }

    public void CloseNote()
    {
        panel.SetActive(false);
        textElement.gameObject.SetActive(false);
        isOpen = false;
        Time.timeScale = 1f;
    }
}