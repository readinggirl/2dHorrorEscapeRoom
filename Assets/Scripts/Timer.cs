using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        var minutes = Mathf.FloorToInt(remainingTime / 60);
        var seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"Time left: {minutes:00}:{seconds:00}";

        if (remainingTime <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}