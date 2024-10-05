using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDDocument
{
    public string name;
    public string job;
    public string expiry;
    public static string[] invalidNames = new string[5] { "Everett Grant", "Lauri Castro", "Edison Mckinney", "Madge Reynolds", "Rochelle Stafford" };

    public IDDocument(string n, bool valid, string reason)
    {
        int day;
        int month;
        int year = 0;
        if (!valid)
        {
            switch (reason) {
                case "Invalid Name":
                    name = invalidNames[Random.Range(0, invalidNames.Length)];
                    break;
                case "Invalid Expiry":
                    year = Random.Range(2000, 2023);
                    break;
                
            }
        }
        else name = n;
        job = "Surveyor";
        day = Random.Range(0, 28);
        month = Random.Range(1, 12);
        if (reason != "Invalid Name") name = n;
        if (reason != "Invalid Expiry") year = Random.Range(2025, 2029);
        expiry = day + "/" + month + "/" + year;
    }
}
