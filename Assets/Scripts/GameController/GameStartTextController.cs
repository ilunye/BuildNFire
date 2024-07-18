using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class GameStartTextController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject audio_321;
    public TMP_Text StartText = null;
    private GameObject player1, player2;

    //voice
    public GameObject BGM;
    public AudioSource BGM_voice;
    void Awake()
    {
        StartText = GetComponent<TMP_Text>();
    }
    void Start()
    {
        player1 = GameObject.Find("animal_people_wolf_1");
        player2 = GameObject.Find("animal_people_wolf_2");
        if (player1 != null && player2 != null)
        {
            player1.GetComponent<Character>().enabled = false;
            player2.GetComponent<Character>().enabled = false;
        }
        StartCoroutine(StartCountdown());
        int bgm_Num = UnityEngine.Random.Range(1, 5);
        switch (bgm_Num)
        {

            case 1:
                BGM = Instantiate(Resources.Load("Audio/bgm1") as GameObject);
                break;
            case 2:
                BGM = Instantiate(Resources.Load("Audio/bgm2") as GameObject);
                break;
            case 3:
                BGM = Instantiate(Resources.Load("Audio/bgm3") as GameObject);
                break;
            case 4:
                BGM = Instantiate(Resources.Load("Audio/bgm4") as GameObject);
                break;
        }

        BGM_voice = BGM.GetComponent<AudioSource>();

    }

    IEnumerator StartCountdown()
    {
        int count = 3;
        RectTransform textRectTransform = StartText.GetComponent<RectTransform>();
        Vector2 startPosition = new Vector2(textRectTransform.anchoredPosition.x, -Screen.height / 2 - 10); // 屏幕底部
        Vector2 middlePosition = new Vector2(textRectTransform.anchoredPosition.x, textRectTransform.anchoredPosition.y); // 屏幕中间
        while (count > 0)
        {
            switch (count)
            {
                case 3:
                    StartText.text = "THREE";
                    // set color
                    StartText.color = new Color(1, 0, 0);
                    break;
                case 2:
                    StartText.text = "TWO";
                    StartText.color = new Color(0, 1, 0);
                    break;
                case 1:
                    StartText.text = "ONE";
                    StartText.color = new Color(0, 0, 1);
                    break;
            }
            float timeToMove = 0.5f;
            float elapsedTime = 0f;
            while (elapsedTime < timeToMove)
            {
                textRectTransform.anchoredPosition = Vector2.Lerp(startPosition, middlePosition, elapsedTime / timeToMove);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            textRectTransform.anchoredPosition = middlePosition;
            yield return new WaitForSeconds(0.5f);
            count--;
        }
        StartText.text = "Start!";
        StartText.color = new Color(1, 1, 1);
        Invoke("Play_BGM", 1f);
        textRectTransform.anchoredPosition = middlePosition;
        float finalMoveTime = 0.5f;
        float finalElapsedTime = 0f;
        while (finalElapsedTime < finalMoveTime)
        {
            textRectTransform.anchoredPosition = Vector2.Lerp(startPosition, middlePosition, finalElapsedTime / finalMoveTime);
            finalElapsedTime += Time.deltaTime;
            yield return null;
        }
        // textRectTransform.anchoredPosition = startPosition;

        float fadeDuration = 0.5f;
        float fadeElapsedTime = 0f;
        while (fadeElapsedTime < fadeDuration)
        {
            StartText.color = new Color(1, 1, 1, 1 - fadeElapsedTime / fadeDuration);
            fadeElapsedTime += Time.deltaTime;
            yield return null;
        }
        StartText.color = new Color(1, 1, 1, 0);
        textRectTransform.anchoredPosition = startPosition;

        if (player1 != null && player2 != null)
        {
            player1.GetComponent<Character>().enabled = true;
            player2.GetComponent<Character>().enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            BGM_voice.Pause();
        }
        else
        {
            BGM_voice.UnPause();
        }
    }



    private void Play_BGM()
    {

        BGM_voice.Play();
    }
}
