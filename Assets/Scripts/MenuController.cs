using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControlller : MonoBehaviour
{
    public GameObject menuCanvas;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
