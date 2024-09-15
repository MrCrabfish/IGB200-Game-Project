using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Character character;

    public int score;

    public TMP_Text scoreText;

    public IDScript ID;

    public Animator anim;

    public GameObject[] charSprites;
    public GameObject charSprite;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        GenerateNewCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Accept();
        if (Input.GetKeyDown(KeyCode.Q)) Deny();
        scoreText.text = "Score: " + score;
        ID.gameObject.SetActive(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
    }

    public void Accept()
    {
        if (character.valid) score += 100;
        else score -= 100;
        anim.SetTrigger("Accept");
        Destroy(charSprite, 0.30f);
        GenerateNewCharacter();
    }

    public void Deny()
    {
        if (!character.valid) score += 100;
        else score -= 100;
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
}
