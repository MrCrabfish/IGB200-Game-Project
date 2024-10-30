using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private GameObject hardHat;
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject headphones;
    [SerializeField] private GameObject glasses;

    public bool head = true;
    public bool mouth = true;
    public bool ears = true;
    public bool eyes = true;

    void Update()
    {
        hardHat.SetActive(head);
        mask.SetActive(mouth);
        headphones.SetActive(ears);
        glasses.SetActive(eyes);
    }

    public void Summon(string role, bool valid)
    {
        if (valid)
        {
            // Assign PPE based on the job role (ensure all required PPE is assigned)
            AssignPPEForJob(role);

            // Randomly add extra PPE, keeping all required PPE
            AddExtraPPE();
        }
        else
        {
            // Assign the required PPE first (valid PPE for the role)
            AssignPPEForJob(role);

            // If the equipment is meant to be invalid, make one piece of required PPE incorrect
            MakePPEInvalidForRole(role);
        }
    }

    // Assigns the required PPE based on the job role
    private void AssignPPEForJob(string role)
    {
        switch (role)
        {
            case "Plumber":
                head = true;
                mouth = true;
                ears = false;
                eyes = false;
                break;
            case "Electrician":
                head = true;
                mouth = false;
                ears = false;
                eyes = true;
                break;
            case "Carpenter":
                head = true;
                mouth = true;
                ears = false;
                eyes = true;
                break;
            case "Bricklayer":
                head = true;
                mouth = false;
                ears = false;
                eyes = true;
                break;
            case "Painter":
                head = true;
                mouth = true;
                ears = false;
                eyes = true;
                break;
            case "Heavy Machine OP":
                head = true;
                mouth = true;
                ears = true;
                eyes = true;
                break;
            case "Welder":
                head = true;
                mouth = true;
                ears = false;
                eyes = true;
                break;
            case "Plasterer":
                head = true;
                mouth = true;
                ears = false;
                eyes = true;
                break;
            case "Concreter":
                head = true;
                mouth = true;
                ears = false;
                eyes = true;
                break;
        }
    }

    // Randomly assigns extra PPE without removing required PPE
    private void AddExtraPPE()
    {
        head = head || (Random.value > 0.5f);
        mouth = mouth || (Random.value > 0.5f);
        ears = ears || (Random.value > 0.5f);
        eyes = eyes || (Random.value > 0.5f);
    }

    // Randomly makes one required piece of PPE incorrect based on the job role
    private void MakePPEInvalidForRole(string role)
    {
        switch (role)
        {
            case "Plumber":
                // Required PPE: head, mouth
                if (Random.value > 0.5f) head = false;
                else mouth = false;
                break;
            case "Electrician":
                // Required PPE: head, eyes
                if (Random.value > 0.5f) head = false;
                else eyes = false;
                break;
            case "Carpenter":
                // Required PPE: head, mouth, eyes
                int carpenterChoice = Random.Range(0, 3);
                if (carpenterChoice == 0) head = false;
                else if (carpenterChoice == 1) mouth = false;
                else eyes = false;
                break;
            case "Bricklayer":
                // Required PPE: head, eyes
                if (Random.value > 0.5f) head = false;
                else eyes = false;
                break;
            case "Painter":
                // Required PPE: head, mouth, eyes
                int painterChoice = Random.Range(0, 3);
                if (painterChoice == 0) head = false;
                else if (painterChoice == 1) mouth = false;
                else eyes = false;
                break;
            case "Heavy Machine OP":
                // Required PPE: head, mouth, ears, eyes
                int machineChoice = Random.Range(0, 4);
                if (machineChoice == 0) head = false;
                else if (machineChoice == 1) mouth = false;
                else if (machineChoice == 2) ears = false;
                else eyes = false;
                break;
            case "Welder":
                // Required PPE: head, mouth, eyes
                int welderChoice = Random.Range(0, 3);
                if (welderChoice == 0) head = false;
                else if (welderChoice == 1) mouth = false;
                else eyes = false;
                break;
            case "Plasterer":
                // Required PPE: head, mouth, eyes
                int plastererChoice = Random.Range(0, 3);
                if (plastererChoice == 0) head = false;
                else if (plastererChoice == 1) mouth = false;
                else eyes = false;
                break;
            case "Concreter":
                // Required PPE: head, mouth, eyes
                int concreterChoice = Random.Range(0, 3);
                if (concreterChoice == 0) head = false;
                else if (concreterChoice == 1) mouth = false;
                else eyes = false;
                break;
        }
    }
}
