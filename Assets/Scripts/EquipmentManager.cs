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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            head = true;
            mouth = true;
            ears = true;
            eyes = true;
        }
        else
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    head = false;
                    break;
                case 1:
                    mouth = false;
                    break;
                case 2:
                    ears = false;
                    break;
                case 3:
                    eyes = false;
                    break;

            }
        }

    }
}