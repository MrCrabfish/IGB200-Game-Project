using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// New GameManager Script
public class GameManagerNew : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public GameObject[] idPrefabs;
    public TMP_Text endOfDayReportText;
    public TMP_Text failureCountText;
    public GameObject endOfDayClipboard;
    public int correctCount;
    public int incorrectCount;
    public int maxFails = 5;
    public GameObject startOfDayClipboard;
    private int currentCharacterIndex;
    private int totalCharacters;

    // Character and ID Instances
    private GameObject currentCharacter;
    private GameObject currentID;
    private EquipmentManager equipmentManager;
    private IDScript idScript;

<<<<<<< Updated upstream
    private void Start()
    {
        endOfDayClipboard.SetActive(false);
        startOfDayClipboard.SetActive(true);
=======
    public AudioSource denySound;
    public AudioSource acceptSound;

    public Scenes sceneManager; // Reference to Scenes script for scene transitions

    public Transform idSpawnPoint;

    private bool canInput = false;

    private void Start()
    {
        endOfDayClipboard.SetActive(false);
        startOfDayClipboard?.SetActive(true);
        failureMessage?.SetActive(false);
>>>>>>> Stashed changes

        totalCharacters = 12; // Set the number of characters to appear during the day
        StartCoroutine(StartDay());
    }

    public Transform idSpawnPoint;

    private IEnumerator StartDay()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        if (startOfDayClipboard != null)
        {
            startOfDayClipboard.SetActive(false);
        }
        // Hide start of day clipboard
        if (endOfDayClipboard != null)
        {
            endOfDayClipboard.SetActive(false);
        }

        yield return new WaitForSeconds(1f); // Optional delay before starting the day

        StartCoroutine(SpawnCharacters());
    }

    private IEnumerator SpawnCharacters()
    {
        for (int i = 0; i < totalCharacters; i++)
        {
            // Select a random character and their respective ID
            currentCharacterIndex = UnityEngine.Random.Range(0, characterPrefabs.Length);
            CharacterDetails characterDetails = new CharacterDetails();

            // Instantiate character and ID using the generated details
            currentCharacter = Instantiate(characterPrefabs[currentCharacterIndex], new Vector3(-12, 0, 0), Quaternion.identity);
            currentID = Instantiate(idPrefabs[currentCharacterIndex], idSpawnPoint.position, Quaternion.identity);

            // Set character and ID validity
            idScript = currentID.GetComponent<IDScript>();
            equipmentManager = currentCharacter.GetComponent<EquipmentManager>();
            idScript.FillDetails(characterDetails);

<<<<<<< Updated upstream
            if (!characterDetails.isValid)
            {
                // Make adjustments for invalid character attributes
                equipmentManager.Summon(characterDetails.job, characterDetails.validEquipment);
            }
=======
            // Set up equipment validity for the character before animation starts
            equipmentManager.Summon(characterDetails.job, characterDetails.isEquipmentValid);
>>>>>>> Stashed changes

            // Debugging: Print character details
            Debug.Log($"Generated Character - Name: {characterDetails.name}, Job: {characterDetails.job}, Expiry Date: {characterDetails.expiryDate}, IsValid: {characterDetails.isValid}, Valid Equipment: {characterDetails.validEquipment}");

            // Wait until character reaches "Idle" state before allowing input
            Animator characterAnimator = currentCharacter.GetComponent<Animator>();
            if (characterAnimator != null)
            {
                yield return new WaitUntil(() => characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
            }

            canInput = true;

            // Wait for player input (accept or reject)
            yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && canInput);
            canInput = false;

<<<<<<< Updated upstream
            // Add a brief delay before destroying the character and ID
            yield return new WaitForSeconds(0.5f);
            Destroy(currentCharacter);
            Destroy(currentID);

            // Add a brief delay before spawning the next character
            yield return new WaitForSeconds(0.5f);
        }
        ShowEndOfDayReport();
        endOfDayClipboard.SetActive(true);
=======
            yield return StartCoroutine(HandleCharacterEvaluation(characterDetails));

            // Check if max fails are reached
            if (incorrectCount >= maxFails)
            {
                Debug.Log("Game Over - Max Fails Reached");
                ShowEndOfDayReport(false);
                yield break;
            }
        }
        ShowEndOfDayReport(true);
>>>>>>> Stashed changes
    }

    private IEnumerator HandleCharacterEvaluation(CharacterDetails characterDetails)
    {
        bool isAccepted = Input.GetKeyDown(KeyCode.E);
        bool isRejected = Input.GetKeyDown(KeyCode.Q);

<<<<<<< Updated upstream
        if ((isAccepted && characterDetails.isValid) || (isRejected && !characterDetails.isValid))
=======
        Animator characterAnimator = currentCharacter.GetComponent<Animator>();
        bool isCharacterValid = EvaluateCharacter(characterDetails);
        bool correctDecision = (isAccepted && isCharacterValid) || (isRejected && !isCharacterValid);

        // Play the appropriate sound based on input
        if (isAccepted)
        {
            acceptSound.Play();
        }
        else if (isRejected)
        {
            denySound.Play(); 
        }

        // Trigger the appropriate animation
        if (isAccepted && characterAnimator != null)
        {
            characterAnimator.SetTrigger("Accept");
        }
        else if (isRejected && characterAnimator != null)
        {
            characterAnimator.SetTrigger("Deny");
        }

        // Update counts based on the correctness of the decision
        if (correctDecision)
>>>>>>> Stashed changes
        {
            correctCount++;
        }
        else
        {
            incorrectCount++;
            UpdateFailureCount();

            if (incorrectCount > maxFails)
            {
                Debug.Log("Game Over");
                StopAllCoroutines(); // Stop the game if max failures are reached
                ShowEndOfDayReport();
            }
        }

<<<<<<< Updated upstream
        // Debugging: Print evaluation details
        Debug.Log($"Evaluation - Accepted: {isAccepted}, Rejected: {isRejected}, Character Valid: {characterDetails.isValid}, Correct Count: {correctCount}, Incorrect Count: {incorrectCount}");
=======
        // Wait until the current animation has fully played before destroying the character
        if (characterAnimator != null)
        {
            yield return new WaitUntil(() => characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Accept") &&
                                               characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 ||
                                               characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Deny") &&
                                               characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        }

        // Destroy the character and the ID after the animation is done
        Destroy(currentCharacter);
        Destroy(currentID);

        // Add a brief delay before spawning the next character
        yield return new WaitForSeconds(0.5f);
    }

    private bool EvaluateCharacter(CharacterDetails characterDetails)
    {
        switch (currentDay)
        {
            case GameDay.Day1:
                // For Day 1, only check name and expiry date
                return characterDetails.isNameValid && characterDetails.isExpiryValid;
            case GameDay.Day2:
                // For Day 2, check name, expiry date, and equipment
                return characterDetails.isNameValid && characterDetails.isExpiryValid && characterDetails.isEquipmentValid;
            case GameDay.Day3:
                // For Day 3, same checks as Day 2 but with more characters
                return characterDetails.isNameValid && characterDetails.isExpiryValid && characterDetails.isEquipmentValid;
            default:
                return false;
        }
>>>>>>> Stashed changes
    }

    private void ShowEndOfDayReport(bool success)
    {
        endOfDayClipboard.SetActive(true);
        endOfDayReportText.text = $"Correct Choices: {correctCount}\nIncorrect Choices: {incorrectCount}";
<<<<<<< Updated upstream
=======

        if (!success)
        {
            failureMessage?.SetActive(true);
            additionalEndOfDayText?.gameObject.SetActive(false);
        }
        else
        {
            additionalEndOfDayText?.gameObject.SetActive(true);
            failureMessage?.SetActive(false);
        }

        StartCoroutine(WaitForEndOfDayInput(success));
    }

    private IEnumerator WaitForEndOfDayInput(bool success)
    {
        // Wait for the player to press Enter to proceed
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        if (success)
        {
            sceneManager.LoadNextDay(); // Trigger the transition to the next scene (Day 2 or Day 3 or credits)
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene if failed
        }
>>>>>>> Stashed changes
    }

    private void UpdateFailureCount()
    {
        if (failureCountText != null)
        {
            failureCountText.text = $"Failures: {incorrectCount}/{maxFails}";
        }
    }
}