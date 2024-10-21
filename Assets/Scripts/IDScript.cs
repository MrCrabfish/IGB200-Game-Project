using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// IDScript for ID Prefabs
public class IDScript : MonoBehaviour
{
    public TMP_Text nameField;
    public TMP_Text jobField;
    public TMP_Text expiryField;

    public void FillDetails(CharacterDetails character)
    {
        nameField.text = character.name;
        jobField.text = character.job;
        expiryField.text = character.expiryDate;
    }
}