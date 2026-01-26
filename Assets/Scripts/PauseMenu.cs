using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject container;

    private bool _isPaused = false;
    // Update is called once per frame
    
    public void Pause() {
        Debug.Log("pause pressed");
        if (_isPaused) {
            Resume();
        }
        else {
            container.SetActive(true);
            Time.timeScale = 0;
            
            _isPaused = true;
        }
    }

    public void Resume() {
        container.SetActive(false);
        Time.timeScale = 1;
            
        _isPaused = false;
    }

    public void MainMenu() {
        SceneManager.LoadScene("StartScene");
    }


    public void Restart() {
        SceneManager.LoadScene(gameObject.scene.name);
    }
}
