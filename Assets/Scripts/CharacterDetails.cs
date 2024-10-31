using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CharacterDetails Class for Handling Character Attributes
public class CharacterDetails
{
    public enum CharacterType { Male, Female, Dog }
    public CharacterType characterType;

    private static string[] maleValidNames = { "Robert Anders", "Doug Hoal", "Pete Hallars", "Owen Sparks", "Bill Derks", "Jack Hammer", "Marcus Plumb" };
    private static string[] femaleValidNames = { "Rebecca Coal", "Bethany Rose", "Hannah Ziller", "Clara Stone" , "Wendy Mason", "Kate Tyler", "Emily Slate" };
    private static string[] maleInvalidNames = { "John Walker", "Elijah Moate", "Samuel Scotts", "Jackson Parker", "Billy Dukes", "Dug Hole", "Robbert Anders" };
    private static string[] femaleInvalidNames = { "Shanda Leer", "Michelle Ryan", "Kylie Raisins", "Hanna Zillar", "Klara Stone", "Bethany Rows", "Carley Mathers" };

    private static string[] jobs = { "Plumber", "Electrician", "Carpenter", "Bricklayer", "Painter", "Heavy Machine OP", "Welder", "Plasterer", "Concreter" };

    public string name;
    public string job;
    public string expiryDate;
    public bool isNameValid;
    public bool isExpiryValid;
    public bool isEquipmentValid;
    public bool isValid;

    public CharacterDetails(GameObject characterPrefab)
    {
        // Determine the character type based on the character prefab name or identifier
        if (characterPrefab.name.Contains("Male"))
        {
            characterType = CharacterType.Male;
        }
        else if (characterPrefab.name.Contains("Female"))
        {
            characterType = CharacterType.Female;
        }
        else if (characterPrefab.name.Contains("Dog"))
        {
            characterType = CharacterType.Dog;
        }

        // Assign validity for each attribute independently
        isNameValid = UnityEngine.Random.value > 0.3f;   // 70% chance of being valid
        isExpiryValid = UnityEngine.Random.value > 0.3f; // 60% chance of being valid
        isEquipmentValid = UnityEngine.Random.value > 0.5f; // 50% chance of being valid

        // Assign name based on character type and validity
        if (characterType == CharacterType.Male)
        {
            name = isNameValid ? maleValidNames[UnityEngine.Random.Range(0, maleValidNames.Length)] :
                                 maleInvalidNames[UnityEngine.Random.Range(0, maleInvalidNames.Length)];
        }
        else if (characterType == CharacterType.Female)
        {
            name = isNameValid ? femaleValidNames[UnityEngine.Random.Range(0, femaleValidNames.Length)] :
                                 femaleInvalidNames[UnityEngine.Random.Range(0, femaleInvalidNames.Length)];
        }
        else if (characterType == CharacterType.Dog)
        {
            // The dog character has a fixed name and doesn't require name validation
            isNameValid = true;
            name = "Rex";
        }

        // Assign expiry date based on validity
        expiryDate = isExpiryValid ? GenerateValidExpiryDate() : GenerateInvalidExpiryDate();

        // Assign job
        job = jobs[UnityEngine.Random.Range(0, jobs.Length)];

        // Overall validity: True if name, expiry, and PPE are all valid
        isValid = isNameValid && isExpiryValid && isEquipmentValid;

        // Debugging
        Debug.Log($"Character Created - Name: {name}, Character Type: {characterType}, Job: {job}, Expiry: {expiryDate}, Name Valid: {isNameValid}, Expiry Valid: {isExpiryValid}, Equipment Valid: {isEquipmentValid}");
    }

    private string GenerateValidExpiryDate()
    {
        int year = UnityEngine.Random.Range(2025, 2031);
        int month = UnityEngine.Random.Range(1, 13);
        int day = UnityEngine.Random.Range(1, 29);
        return $"{day:00}/{month:00}/{year}";
    }

    private string GenerateInvalidExpiryDate()
    {
        int year = UnityEngine.Random.Range(2000, 2024);
        int month = UnityEngine.Random.Range(1, 13);
        int day = UnityEngine.Random.Range(1, (month == 8) ? 16 : 29);
        return $"{day:00}/{month:00}/{year}";
    }
}
