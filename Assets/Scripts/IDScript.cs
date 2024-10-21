using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class IDScript : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text job;
    public TMP_Text expiry;

    public void fillDetails(Character character)
    {
        name.text = character.ID.name;
        job.text = character.ID.job;
        expiry.text = character.ID.expiry;
    }
}