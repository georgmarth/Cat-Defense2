using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuPanel;

    public static PauseMenu pauseMenu;

    public bool pause { get; private set;}

    private void Awake()
    {
        if (pauseMenu != null)
            Destroy(gameObject);
        pauseMenu = this;
        pause = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        pause = pauseMenuPanel.activeSelf;
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

}
