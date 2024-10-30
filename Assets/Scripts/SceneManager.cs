using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public AudioSource popSound;
    private bool endOfDayRevealed = false;  // Variable to track if the end of day clipboard has been revealed

    void Update()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // If the current scene is the title or tutorial, pressing Enter moves to the next scene
            if (currentScene.buildIndex == 0 || currentScene.buildIndex == 1)
            {
                StartGame();
            }
            // For Day scenes, allow pressing Enter only if the end of day clipboard is revealed
            else if (endOfDayRevealed)
            {
                LoadNextDay();
            }
        }
    }

    public void ShowEndOfDayClipboard()
    {
        endOfDayRevealed = true;
    }

    public void StartGame()
    {
        // Play the pop sound effect
        if (popSound != null) popSound.Play();

        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Load the appropriate scene based on the current one
        if (currentScene.buildIndex == 0) // Assuming the title screen is Scene 0
        {
            SceneManager.LoadScene(1); // Load the tutorial scene (Scene 1)
        }
        else if (currentScene.buildIndex == 1) // Assuming the tutorial is Scene 1
        {
            SceneManager.LoadScene(2); // Load the Day 1 scene (Scene 2)
        }
    }

    public void LoadNextDay()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Load the next scene based on the current day
        if (currentScene.buildIndex == 2) // Day 1
        {
            SceneManager.LoadScene(3); // Load Day 2 (Scene 3)
        }
        else if (currentScene.buildIndex == 3) // Day 2
        {
            SceneManager.LoadScene(4); // Load Day 3 (Scene 4)
        }
        else if (currentScene.buildIndex == 4) // Day 3
        {
            SceneManager.LoadScene(5); // Load Credits scene (Scene 5)
        }
    }
}