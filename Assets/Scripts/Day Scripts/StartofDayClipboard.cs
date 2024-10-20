using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideClipboard : MonoBehaviour
{
    public GameObject clipboard; // Assign the Start of Day Clipboard GameObject in the Inspector
    private float timeElapsed = 0f; // Timer to track elapsed time

    void Update()
    {
        // Increment the timer
        timeElapsed += Time.deltaTime;

        // Check if spacebar is pressed and at least 2 seconds have passed
        if (Input.GetKeyDown(KeyCode.Space) && timeElapsed >= 2f)
        {
            HideClipboardObject();
        }
    }

    void HideClipboardObject()
    {
        if (clipboard != null)
        {
            clipboard.SetActive(false); // Hides the clipboard
        }
        else
        {
            Debug.LogWarning("Clipboard GameObject is not assigned.");
        }
    }
}