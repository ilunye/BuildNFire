using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;
using System;
using System.Net;

public class sum_msg : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button;
    public int status = 0;  // 0: not end, 1: player 1 won, 2: player 2 won
    public TMP_Text theText = null;
    GameObject player1, player2;

    public GameObject explode_bomb;

    private bool gameover = false;

    private GameObject ending;
    private AudioSource ending_source;

    private GameObject endAnimation;
    private GameObject fire;

    private GameObject bigBomb;
    private AudioSource bigBomb_voice;
    public void ToMenu()
    {
        Application.LoadLevel("Scenes/HomeScreen");
    }
    void Awake()
    {
        theText = GetComponent<TMP_Text>();
        player1 = GameObject.Find("PlayerUI_1");
        player2 = GameObject.Find("PlayerUI_2");
    }
    void Start()
    {
        button.SetActive(false);
        ending = Instantiate(Resources.Load("Audio/ending") as GameObject);
        ending_source = ending.GetComponent<AudioSource>();
        bigBomb = Instantiate(Resources.Load("Audio/bigbomb") as GameObject);
        bigBomb_voice = bigBomb.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<WorkFlow>().iron_number == 6 && player1.GetComponent<WorkFlow>().wood_number == 4 && player1.GetComponent<WorkFlow>().gunpowder_number == 1 && player1.GetComponent<WorkFlow>().projectile_number == 1)
        {
            status = 1;
        }
        else if (player2.GetComponent<WorkFlow>().iron_number == 6 && player2.GetComponent<WorkFlow>().wood_number == 4 && player2.GetComponent<WorkFlow>().gunpowder_number == 1 && player2.GetComponent<WorkFlow>().projectile_number == 1)
        {
            status = 2;
        }
        if (status == 1 && !gameover)
        {
            theText.text = "PLAYER ONE WON!";
            button.SetActive(true);
            Play_Final();
            gameover = true;
            endAnimation = Instantiate(Resources.Load("Prefabs/explode") as GameObject);
            fire = Instantiate(Resources.Load("Prefabs/fire") as GameObject);
            Vector3 endPosition = new Vector3(805f, 0f, 981f);
            endAnimation.transform.position = endPosition;
            fire.transform.position = endPosition;
            ending_source.Play();
            bigBomb_voice.Play();
            Collide_Arround(endPosition);
        }
        else if (status == 2 && !gameover)
        {
            theText.text = "PLAYER TWO WON!";
            button.SetActive(true);
            Play_Final();
            gameover = true;
            endAnimation = Instantiate(Resources.Load("Prefabs/explode") as GameObject);
            fire = Instantiate(Resources.Load("Prefabs/fire") as GameObject);
            Vector3 endPosition = new Vector3(796f, 0f, 981f);
            endAnimation.transform.position = endPosition;
            fire.transform.position = endPosition;
            ending_source.Play();
            bigBomb_voice.Play();
            Collide_Arround(endPosition);
        }

    }

    private void Collide_Arround(Vector3 endposition)
    {
        float explosionRadius = 4f;
        float explosionForce = 700f;
        // 获取爆炸范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(endposition, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }


    private void Play_Final()
    {
        Debug.Log("play final");
        explode_bomb = Instantiate(Resources.Load("Prefabs/projectile1") as GameObject);
        StartCoroutine(MoveBomb());
    }

    private IEnumerator MoveBomb()
    {
        Vector3 startposition;
        Vector3 targetposition;
        if (status == 1)
        {
            startposition = new Vector3(805f, 1.6f, 981f);
            targetposition = new Vector3(796f, 1.6f, 981f);
        }
        else
        {

            startposition = new Vector3(796f, 1.6f, 981f);
            targetposition = new Vector3(805f, 1.6f, 981f);
        }

        float speed = 1.0f;
        float t = 0f;
        while (t < 2f)
        {
            t += Time.deltaTime * speed;
            explode_bomb.transform.position = Vector3.Lerp(startposition, targetposition, t);
            yield return null;
        }

        //Debug.Log(explode_bomb.transform.position);

    }

}



