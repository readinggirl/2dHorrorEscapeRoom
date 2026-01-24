using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSequence : MonoBehaviour
{
    public TextMeshProUGUI[] tutorialTexts;
    public string nextSceneName = "Indoor";
    private int currentIndex = 0;

    void Start()
    {
        // SAFETY CHECK
        if (tutorialTexts == null || tutorialTexts.Length == 0)
        {
            Debug.LogError("TutorialSequence: No tutorial texts assigned in the Inspector!");
            return;
        }

        // Make sure only the first text is visible
        for (int i = 0; i < tutorialTexts.Length; i++)
        {
            tutorialTexts[i].gameObject.SetActive(i == 0);
        }

        Debug.Log("Tutorial started. Showing first text.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Enter pressed");
            NextText();
        }
    }

    void NextText()
    {
        // If we are already at the last text, finish tutorial
        if (currentIndex >= tutorialTexts.Length - 1)
        {
            tutorialTexts[currentIndex].gameObject.SetActive(false);
            Debug.Log("Tutorial finished");
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        // Hide current text
        tutorialTexts[currentIndex].gameObject.SetActive(false);

        // Show next text
        currentIndex++;
        tutorialTexts[currentIndex].gameObject.SetActive(true);
        Debug.Log("Showing text index: " + currentIndex);
    }
}
