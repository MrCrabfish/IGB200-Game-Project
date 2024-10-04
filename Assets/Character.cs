using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public static string[] names = new string[5] {"Elijah Stevenson", "Marianne Farrell", "Grady Cervantes", "Huey Frost", "Imelda Cantu"};
    public string name;
    public bool valid;
    public static string[] invalidityReasons = new string[3] { "Invalid Name", "Invalid Expiry", "Invalid equipment"};
    public string invalidReason = "None";
    public IDDocument ID;

    public Character()
    {
        name = names[Random.Range(0, names.Length)];
        valid = Random.value > 0.5;
        if (!valid)
        {
            invalidReason = invalidityReasons[Random.Range(0, invalidityReasons.Length)];
        }
        Debug.Log(invalidReason);
        ID = new IDDocument(name, valid, invalidReason);
    }

}
