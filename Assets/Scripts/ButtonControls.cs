using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControls : MonoBehaviour {
    public GameObject creditCanvas;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowCredits() {
        creditCanvas.SetActive(true);
    }

    public void HideCredits() {
        Debug.Log("hidecredits");
        creditCanvas.SetActive(false);
    }
}