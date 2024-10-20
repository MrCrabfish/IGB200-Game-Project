using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IDScript : MonoBehaviour
{
    public TMP_Text characterNameText; // Renamed from 'name' to 'characterNameText'
    public TMP_Text job;
    public TMP_Text expiry;

    public void fillDetails(Character character)
    {
        characterNameText.text = character.ID.name; // Updated to use the new variable name
        job.text = character.ID.job;
        expiry.text = character.ID.expiry;
    }
}