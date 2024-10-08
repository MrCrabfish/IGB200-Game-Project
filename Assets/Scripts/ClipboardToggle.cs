using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardToggle : MonoBehaviour
{
    // Boolean to track whether the clipboard is currently visible or not
    private bool isVisible = true;

    // Boolean to track whether the player can toggle the clipboard yet
    private bool canToggle = false;

    // Reference to the License text GameObject
    public GameObject license; // Assign this in the Inspector

    void Start()
    {
        // Start the coroutine that will allow toggling after 3 seconds
        StartCoroutine(EnableToggleAfterDelay(3f));

        // Initially hide the License GameObject
        license.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Only allow toggling if the 3-second delay has passed
        if (canToggle && Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle the visibility of the clipboard
            isVisible = !isVisible;

            // Enable or disable the clipboard GameObject
            gameObject.SetActive(isVisible);

            // Show the License text only if the clipboard is hidden
            if (!isVisible)
            {
                license.SetActive(true);
            }
        }
    }

    // Coroutine to enable toggling after a specified delay
    IEnumerator EnableToggleAfterDelay(float delay)
    {
        // Wait for the specified amount of time (3 seconds)
        yield return new WaitForSeconds(delay);

        // Allow the clipboard to be toggled
        canToggle = true;
    }
}