using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PauseMenu;
    public GameObject PauseButton;
    void Start()
    {
        PauseMenu = GameObject.Find("Canvas/PauseMenu");
        PauseButton = GameObject.Find("Canvas/PauseButton");
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause()
    {
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnBackToMenu()
    {
        //Loading Scene0
        PauseButton.SetActive(false);
        PauseMenu.SetActive(false);
        Application.LoadLevel("Scenes/HomeScreen");
    }

}
