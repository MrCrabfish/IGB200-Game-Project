using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Character character;

    public int score;
    public int remaining;
    public int fails;
    public int failLimit = 5;
    public float timer;
    public float timeLimit = 300f;

    public TMP_Text scoreText;

    public IDScript ID;

    public Animator anim;
    public EquipmentManager equipment;

    public GameObject[] charSprites;
    public GameObject charSprite;

    public AudioSource popSound;

    public int dayCounter = 0;

    private bool gameRunning = true;
    private bool loss = false;
    // Start is called before the first frame update
    void Start()
    {
        StartDay();
        GenerateNewCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            if (Input.GetKeyDown(KeyCode.E)) Accept();
            if (Input.GetKeyDown(KeyCode.Q)) Deny();
            scoreText.text = "Score: " + score;
            if (anim != null) ID.gameObject.SetActive(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"));

            if(remaining <= 0)
            {
                Debug.Log("Complete");
                gameRunning = false;
            }

            if (Time.time >= timer + timeLimit)
            {
                Debug.Log("Day end");
                fails += remaining;
                gameRunning = false;
            }

            if (fails > failLimit)
            {
                Debug.Log("Game Over");
                loss = true;
                gameRunning = false;
            }
        }
        else if (!loss) StartDay();
    }

    public void Accept()
    {
        popSound.Play();
        if (character.valid) score++;
        else fails++;
        anim.SetTrigger("Accept");
        Destroy(charSprite, 0.30f);
        remaining -= 1;
        GenerateNewCharacter();
    }

    public void Deny()
    {
        popSound.Play();
        if (!character.valid) score++;
        else fails++;
        anim.SetTrigger("Deny");
        Destroy(charSprite, 0.30f);
        remaining -= 1;
        GenerateNewCharacter();
    }

    public void GenerateNewCharacter()
    {
        character = new Character();
        ID.fillDetails(character);
        ID.gameObject.SetActive(false);
        StartCoroutine(instantiation(0.2f));
    }

    IEnumerator instantiation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        charSprite = Instantiate(charSprites[Random.Range(0, charSprites.Length)], new Vector3(-12, 0, 0), Quaternion.identity);
        equipment = charSprite.GetComponent<EquipmentManager>();
        equipment.Summon(character.ID.job, character.invalidReason != "Invalid equipment" || dayCounter < 3);
        if (dayCounter < 3 && character.invalidReason == "Invalid equipment") character.valid = true;

        anim = charSprite.GetComponent<Animator>();
    }

    public void StartDay()
    {
        Debug.Log("Day Start");
        dayCounter += 1;
        timer = Time.time;
        fails = 0;
        score = 0;
        remaining = 10 + (2 * dayCounter);
        gameRunning = true;
    }
}
