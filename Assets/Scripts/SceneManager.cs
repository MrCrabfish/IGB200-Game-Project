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
            // For Scene 0 (title) or Scene 1 (tutorial), allow pressing Enter to move to the next scene
            if (currentScene.buildIndex == 0 || currentScene.buildIndex == 1)
            {
                StartGame();
            }
            // For Scene 2 (Day 1), only allow pressing Enter if the end of day clipboard is revealed
            else if (currentScene.buildIndex == 2 && endOfDayRevealed)
            {
                StartGame();
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
        popSound.Play();

        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Check if we are currently in the title or tutorial scene
        if (currentScene.buildIndex == 0) // Assuming the title screen is Scene 0
        {
            SceneManager.LoadScene(1); // Load the tutorial scene (Scene 1)
        }
        else if (currentScene.buildIndex == 1) // Assuming the tutorial is Scene 1
        {
            SceneManager.LoadScene(2); // Load the Day 1 scene (Scene 2)
        }
        else if (currentScene.buildIndex == 2) // Assuming Day 1 is Scene 2
        {
            SceneManager.LoadScene(0); // Load the title screen again temp
        }
    }
}