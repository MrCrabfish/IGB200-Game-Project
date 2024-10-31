using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerNew : MonoBehaviour
{
    public enum GameDay { Day1, Day2, Day3 }
    public GameDay currentDay;

    public GameObject[] characterPrefabs;
    public GameObject[] idPrefabs;
    public TMP_Text endOfDayReportText;
    public TMP_Text additionalEndOfDayText;
    public TMP_Text failureCountText;
    public GameObject endOfDayClipboard;
    public GameObject startOfDayClipboard;
    public GameObject failureMessage;
    public int correctCount;
    public int incorrectCount;
    public int maxFails = 5;
    private int totalCharacters;

    // Character and ID Instances
    private GameObject currentCharacter;
    private GameObject currentID;
    private EquipmentManager equipmentManager;
    private IDScript idScript;

    public AudioSource denySound;
    public AudioSource acceptSound;

    public Scenes sceneManager; // Reference to Scenes script for scene transitions

    public Transform idSpawnPoint;

    private void Start()
    {
        endOfDayClipboard.SetActive(false);
        startOfDayClipboard.SetActive(true);
        failureMessage.SetActive(false);

        ConfigureDaySettings();
        StartCoroutine(StartDay());
    }

    private void ConfigureDaySettings()
    {
        // Configure settings based on the current day
        switch (currentDay)
        {
            case GameDay.Day1:
                totalCharacters = 12; // Number of characters for Day 1
                break;
            case GameDay.Day2:
                totalCharacters = 12; // Number of characters for Day 2, with equipment check
                break;
            case GameDay.Day3:
                totalCharacters = 18; // More characters for Day 3
                break;
        }
    }

    private IEnumerator StartDay()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        if (startOfDayClipboard != null)
        {
            startOfDayClipboard.SetActive(false);
        }

        yield return new WaitForSeconds(1f); // Optional delay before starting the day

        StartCoroutine(SpawnCharacters());
    }

    private IEnumerator SpawnCharacters()
    {
        for (int i = 0; i < totalCharacters; i++)
        {
            // Select a random character and their respective ID
            int currentCharacterIndex = UnityEngine.Random.Range(0, characterPrefabs.Length);
            GameObject characterPrefab = characterPrefabs[currentCharacterIndex];

            // Instantiate character and ID using the generated details
            currentCharacter = Instantiate(characterPrefab, new Vector3(-12, 0, 0), Quaternion.identity);
            currentID = Instantiate(idPrefabs[currentCharacterIndex], idSpawnPoint.position, Quaternion.identity);

            // Create CharacterDetails instance with the characterPrefab as an argument
            CharacterDetails characterDetails = new CharacterDetails(characterPrefab);

            // Set character and ID validity
            idScript = currentID.GetComponent<IDScript>();
            equipmentManager = currentCharacter.GetComponent<EquipmentManager>();
            idScript.FillDetails(characterDetails);

            // Set up equipment validity for the character
            equipmentManager.Summon(characterDetails.job, characterDetails.isEquipmentValid);

            // Debugging: Print character details
            Debug.Log($"Generated Character - Name: {characterDetails.name}, Job: {characterDetails.job}, Expiry Date: {characterDetails.expiryDate}, Name Valid: {characterDetails.isNameValid}, Expiry Valid: {characterDetails.isExpiryValid}, Equipment Valid: {characterDetails.isEquipmentValid}");

            // Wait for player input (accept or reject)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
            HandleCharacterEvaluation(characterDetails);

            // Add a brief delay before destroying the character and ID
            yield return new WaitForSeconds(0.5f);
            Destroy(currentCharacter);
            Destroy(currentID);

            // Add a brief delay before spawning the next character
            yield return new WaitForSeconds(0.5f);

            // Check if max fails are reached
            if (incorrectCount >= maxFails)
            {
                Debug.Log("Game Over - Max Fails Reached");
                ShowEndOfDayReport();
                ShowFailureMessage();
                yield break;
            }
        }
        ShowEndOfDayReport();
    }

    private void HandleCharacterEvaluation(CharacterDetails characterDetails)
    {
        bool isAccepted = Input.GetKeyDown(KeyCode.E);
        bool isRejected = Input.GetKeyDown(KeyCode.Q);

        bool isCharacterValid = EvaluateCharacter(characterDetails);

        if ((isAccepted && isCharacterValid) || (isRejected && !isCharacterValid))
        {
            correctCount++;
            acceptSound.Play();
        }
        else
        {
            incorrectCount++;
            denySound.Play();
            UpdateFailureCount();
        }

        // Debugging: Print evaluation details
        Debug.Log($"Evaluation - Accepted: {isAccepted}, Rejected: {isRejected}, Character Valid: {isCharacterValid}, Correct Count: {correctCount}, Incorrect Count: {incorrectCount}");
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
    }

    private void ShowEndOfDayReport()
    {
        endOfDayClipboard.SetActive(true);
        endOfDayReportText.text = $"Correct Choices: {correctCount}\nIncorrect Choices: {incorrectCount}";

        if (incorrectCount >= maxFails)
        {
            additionalEndOfDayText.gameObject.SetActive(true);
        }
        else
        {
            additionalEndOfDayText.gameObject.SetActive(false);
        }

        // Start waiting for the player to press Enter to proceed to the next scene
        StartCoroutine(WaitForEndOfDayInput(success: true));
    }

    private void ShowFailureMessage()
    {
        failureMessage.SetActive(true);
        StartCoroutine(WaitForEndOfDayInput(success: false));
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
    }

    private void UpdateFailureCount()
    {
        if (failureCountText != null)
        {
            failureCountText.text = $"Failures: {incorrectCount}/{maxFails}";
        }
    }
}
