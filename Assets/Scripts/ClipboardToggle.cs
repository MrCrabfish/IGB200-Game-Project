using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardToggle : MonoBehaviour
{
    // Booleans to track the current state
    // Removed the unused isVisible variable
    private bool canToggle = false; // Check if toggling is enabled

    // References to the speech bubbles, clipboard, license and move on button
    public GameObject Clipboard; // Clipbaord Object
    public GameObject license; // License object (ID / Worker Speech Bubble)
    public GameObject buttonsSpeechBubble; // Buttons speech bubble
    public GameObject idSpeechBubble; // ID speech bubble
    public GameObject computerSpeechBubble; // Computer speech bubble
    public GameObject moveOnButton; // Move on button

    // Reference to the AudioSource for the sound effect
    public AudioSource popSound; // Assign this in the Inspector

    private int state = 0; // Track the progress of spacebar presses

    void Start()
    {
        // Start coroutine to enable toggling after a 3-second delay
        StartCoroutine(EnableToggleAfterDelay(3f));

        // Initially hide all objects except the clipboard
        license.SetActive(false);
        buttonsSpeechBubble.SetActive(false);
        idSpeechBubble.SetActive(false);
        computerSpeechBubble.SetActive(false);
        moveOnButton.SetActive(false);
    }

    void Update()
    {
        // Only allow progressing through the sequence after 3 seconds
        if (canToggle && Input.GetKeyDown(KeyCode.Space))
        {
            HandleSpaceBarPress();
        }
    }

    void HandleSpaceBarPress()
    {
        // Play the pop sound when the spacebar is pressed
        if (popSound != null)
        {
            popSound.Play();
        }

        Debug.Log("Spacebar pressed, current state: " + state); // Debug log for tracking state

        switch (state)
        {
            case 0: // Hide the clipboard and show the Buttons speech bubble
                Clipboard.SetActive(false); // Hide clipboard
                buttonsSpeechBubble.SetActive(true); // Show Buttons speech bubble
                break;

            case 1: // Show the ID/Worker speech bubble
                buttonsSpeechBubble.SetActive(false); // Hide Buttons speech bubble
                idSpeechBubble.SetActive(true); // Show ID/Worker speech bubble
                license.SetActive(true); // Show License at the same time
                break;

            case 2: // Show the Computer speech bubble
                idSpeechBubble.SetActive(false); // Hide ID/Worker speech bubble
                computerSpeechBubble.SetActive(true); // Show Computer speech bubble
                break;

            case 3: // Hide the Computer speech bubble and show the Move On button
                computerSpeechBubble.SetActive(false); // Hide Computer speech bubble
                license.SetActive(false); // Hide License because the letters will not cooperate with me
                moveOnButton.SetActive(true); // Show Move On button
                break;
        }

        // Move to the next state
        state++;
    }

    // Coroutine to enable toggling after a specified delay
    IEnumerator EnableToggleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canToggle = true; // Allow the spacebar to be pressed after the delay
        Debug.Log("Toggle enabled"); // Debug log to confirm toggle is enabled
    }
}