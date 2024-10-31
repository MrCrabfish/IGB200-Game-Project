using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject jobMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void JobInfo()
    {
        jobMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        jobMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
