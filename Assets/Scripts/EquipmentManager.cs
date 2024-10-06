using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    [SerializeField] private GameObject hardHat;
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject headphones;

    public bool head = true;
    public bool mouth = true;
    public bool ears = true;

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
    }

    public void Summon(string role, bool valid)
    {
        if (valid)
        {
            head = true;
            mouth = true;
            ears = true;
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
                    
            }
        }

    }
}
