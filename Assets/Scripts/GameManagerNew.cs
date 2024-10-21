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

    private void Start()
    {
        endOfDayClipboard.SetActive(false);
        startOfDayClipboard.SetActive(true);

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

            if (!characterDetails.isValid)
            {
                // Make adjustments for invalid character attributes
                equipmentManager.Summon(characterDetails.job, characterDetails.validEquipment);
            }

            // Debugging: Print character details
            Debug.Log($"Generated Character - Name: {characterDetails.name}, Job: {characterDetails.job}, Expiry Date: {characterDetails.expiryDate}, IsValid: {characterDetails.isValid}, Valid Equipment: {characterDetails.validEquipment}");

            // Wait for player input (accept or reject)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
            HandleCharacterEvaluation(characterDetails);

            // Add a brief delay before destroying the character and ID
            yield return new WaitForSeconds(0.5f);
            Destroy(currentCharacter);
            Destroy(currentID);

            // Add a brief delay before spawning the next character
            yield return new WaitForSeconds(0.5f);
        }
        ShowEndOfDayReport();
        endOfDayClipboard.SetActive(true);
    }

    private void HandleCharacterEvaluation(CharacterDetails characterDetails)
    {
        bool isAccepted = Input.GetKeyDown(KeyCode.E);
        bool isRejected = Input.GetKeyDown(KeyCode.Q);

        if ((isAccepted && characterDetails.isValid) || (isRejected && !characterDetails.isValid))
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

        // Debugging: Print evaluation details
        Debug.Log($"Evaluation - Accepted: {isAccepted}, Rejected: {isRejected}, Character Valid: {characterDetails.isValid}, Correct Count: {correctCount}, Incorrect Count: {incorrectCount}");
    }

    private void ShowEndOfDayReport()
    {
        endOfDayClipboard.SetActive(true);
        endOfDayReportText.text = $"Correct Choices: {correctCount}\nIncorrect Choices: {incorrectCount}";
    }

    private void UpdateFailureCount()
    {
        if (failureCountText != null)
        {
            failureCountText.text = $"Failures: {incorrectCount}/{maxFails}";
        }
    }
}