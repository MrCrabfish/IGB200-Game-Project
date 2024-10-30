using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CharacterDetails Class for Handling Character Attributes
public class CharacterDetails
{
    private static string[] validNames = { "Robert Anders", "Rebecca Coal", "Bethany Rose", "Hannah Ziller", "Douge Hoal", "Pete Hallars" };
    private static string[] invalidNames = { "Shanda Leer", "Michelle Ryan", "Kylie Raisins", "John Walker", "Elijah Moate", "Samuel Scotts" };
    private static string[] jobs = { "Plumber", "Electrician", "Carpenter", "Bricklayer", "Painter", "Heavy Machine OP", "Welder", "Plasterer", "Concreter" };

    public string name;
    public string job;
    public string expiryDate;
    public bool isNameValid;
    public bool isExpiryValid;
    public bool isEquipmentValid;
    public bool isValid;

    public CharacterDetails()
    {
        // Set validity for name, expiry date, and equipment (with higher probability of being valid)
        isNameValid = UnityEngine.Random.value > 0.3f;   // 70% chance of being valid
        isExpiryValid = UnityEngine.Random.value > 0.3f; // 70% chance of being valid
        isEquipmentValid = UnityEngine.Random.value > 0.5f; // 50% chance of being valid

        // Assign name validity
        name = isNameValid ? validNames[UnityEngine.Random.Range(0, validNames.Length)] : invalidNames[UnityEngine.Random.Range(0, invalidNames.Length)];

        // Assign expiry validity
        expiryDate = isExpiryValid ? GenerateValidExpiryDate() : GenerateInvalidExpiryDate();

        // Assign job (equipment validity will be determined separately)
        job = jobs[UnityEngine.Random.Range(0, jobs.Length)];

        // Determine overall validity
        isValid = isNameValid && isExpiryValid && isEquipmentValid;

        // Debugging to check validity assignment
        Debug.Log($"Character Created - Name: {name}, Job: {job}, Expiry: {expiryDate}, Name Valid: {isNameValid}, Expiry Valid: {isExpiryValid}, Equipment Valid: {isEquipmentValid}");
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
