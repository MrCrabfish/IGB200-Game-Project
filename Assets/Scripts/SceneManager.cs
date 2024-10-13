using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public AudioSource popSound;
    private bool tutorialLoaded = false;  // Variable to track if the tutorial has already been loaded

    void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        // Play the pop sound effect
        popSound.Play();

        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Check if we are currently in the tutorial scene
        if (currentScene.buildIndex == 0) // Assuming the tutorial is Scene 0
        {
            SceneManager.LoadScene(2); // Load the gameplay scene (Scene 2)
        }
        else if (currentScene.buildIndex == 1) // Assuming the title screen is Scene 1
        {
            SceneManager.LoadScene(0); // Load the tutorial scene (Scene 0)
        }
    }
}
