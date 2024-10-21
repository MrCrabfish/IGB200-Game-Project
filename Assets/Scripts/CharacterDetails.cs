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
    public bool isValid;
    public bool validEquipment;

    public CharacterDetails()
    {
        isValid = UnityEngine.Random.value > 0.5f;

        if (isValid)
        {
            name = validNames[UnityEngine.Random.Range(0, validNames.Length)];
            expiryDate = GenerateValidExpiryDate();
            job = jobs[UnityEngine.Random.Range(0, jobs.Length)];
            validEquipment = true;
        }
        else
        {
            name = invalidNames[UnityEngine.Random.Range(0, invalidNames.Length)];
            expiryDate = GenerateInvalidExpiryDate();
            job = jobs[UnityEngine.Random.Range(0, jobs.Length)];
            validEquipment = UnityEngine.Random.value > 0.5f;
        }

        // Debugging to check validity assignment
        Debug.Log($"Character Created - Name: {name}, Job: {job}, Expiry: {expiryDate}, Valid: {isValid}");
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