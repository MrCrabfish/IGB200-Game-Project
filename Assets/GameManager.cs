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
    public float timer;
    public float timeLimit = 300f;

    public TMP_Text scoreText;

    public IDScript ID;

    public Animator anim;

    public GameObject[] charSprites;
    public GameObject charSprite;

    public AudioSource popSound;

    public int dayCounter = 1;

    private bool gameRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;
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
            ID.gameObject.SetActive(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"));

            if (Time.time >= timer + timeLimit)
            {
                Debug.Log("Game Over");
                gameRunning = false;
            }
        }
    }

    public void Accept()
    {
        popSound.Play();
        if (character.valid) score++;
        else fails++;
        anim.SetTrigger("Accept");
        Destroy(charSprite, 0.30f);
        GenerateNewCharacter();
    }

    public void Deny()
    {
        popSound.Play();
        if (!character.valid) score++;
        else fails++;
        anim.SetTrigger("Deny");
        Destroy(charSprite, 0.30f);
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
        anim = charSprite.GetComponent<Animator>();
    }

    public void StartDay()
    {
        timer = Time.time;
        fails = 0;
        score = 0;
        remaining = 10;
    }
}
