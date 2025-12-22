using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldGrabber : MonoBehaviour {
    [Header("The value we got from the input field")] [SerializeField]
    private string inputText;

    [Header("Showing the reaction to the player")] [SerializeField]
    private GameObject reactionGroup;

    [SerializeField] private TMP_Text reactionTextBox;

    // This is called by the Input Field (On Value Changed or On End Edit)
    public void GrabFromInputField(string input) {
        inputText = input;
        DisplayReactionToInput();
    }

    private void DisplayReactionToInput() {
        reactionGroup.SetActive(true);

        if (inputText == "asdf") {
            reactionTextBox.text = "You escaped!";
        }
        else {
            reactionTextBox.text = "Try again.";
        }
    }
}