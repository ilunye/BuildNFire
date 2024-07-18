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
    private BombTrigger bombTrigger;

    private ShakeCamera shakeCamera;
    public int scenceID = 1;

    public float changeScale = 1f;

    [Obsolete]

    public void ToMenu()
    {
        Application.LoadLevel("Scenes/HomeScreen");
    }
    void Awake()
    {
        theText = GetComponent<TMP_Text>();
        player1 = GameObject.Find("PlayerUI_1");
        player2 = GameObject.Find("PlayerUI_2");

        bombTrigger = GetComponent<BombTrigger>();
    }
    void Start()
    {
        button.SetActive(false);
        ending = Instantiate(Resources.Load("Audio/ending") as GameObject);
        ending_source = ending.GetComponent<AudioSource>();
        bigBomb = Instantiate(Resources.Load("Audio/bigbomb") as GameObject);
        bigBomb_voice = bigBomb.GetComponent<AudioSource>();
        shakeCamera = GameObject.Find("MainCamera").GetComponent<ShakeCamera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<WorkFlow>().iron_number == 3 && player1.GetComponent<WorkFlow>().wood_number == 2 && player1.GetComponent<WorkFlow>().gunpowder_number == 1 && player1.GetComponent<WorkFlow>().projectile_number == 1)
        {
            status = 1;
        }
        else if (player2.GetComponent<WorkFlow>().iron_number == 3 && player2.GetComponent<WorkFlow>().wood_number == 2 && player2.GetComponent<WorkFlow>().gunpowder_number == 1 && player2.GetComponent<WorkFlow>().projectile_number == 1)
        {
            status = 2;
        }
        if (status == 1 && !gameover)
        {
            theText.text = "PLAYER ONE WON!";

            button.SetActive(true);
            Play_Final();
            
            gameover = true;
        }
        else if (status == 2 && !gameover)
        {
            theText.text = "PLAYER TWO WON!";
            button.SetActive(true);
            Play_Final();
            
            gameover = true;
        }

    }
    private void Collide_Arround(Vector3 endposition)
    {
        float explosionRadius = 20f;
        float explosionForce = 200f;
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
        explode_bomb = Instantiate(Resources.Load("Prefabs/projectile1") as GameObject);
        explode_bomb.transform.localScale *= changeScale;

        StartCoroutine(MoveBomb());
    }

    private IEnumerator MoveBomb()
    {
        Vector3 startposition;
        Vector3 targetposition;
        switch (scenceID)
        {

            case 1: //main scene
                if (status == 2)
                {
                    startposition = new Vector3(804.42f, 1.6f, 981f);
                    targetposition = new Vector3(794.84f, 1.6f, 981f);
                }
                else
                {

                    startposition = new Vector3(794.84f, 1.6f, 981f);
                    targetposition = new Vector3(804.42f, 1.6f, 981f);
                }

                break;
            case 2: //city scene
                if (status == 1)
                {
                    startposition = new Vector3(161f, 3.3f, 140f);
                    targetposition = new Vector3(197f, 3.43f, 139f);
                }
                else
                {

                    startposition = new Vector3(197f, 3.43f, 139f);
                    targetposition = new Vector3(161f, 3.3f, 140f);
                }
                break;
            default: //other scene
                if (status == 2)
                {
                    startposition = new Vector3(804.42f, 1.6f, 981f);
                    targetposition = new Vector3(794.84f, 1.6f, 981f);
                }
                else
                {

                    startposition = new Vector3(794.84f, 1.6f, 981f);
                    targetposition = new Vector3(804.42f, 1.6f, 981f);
                }
                break;

        }


        float speed = 1.0f;
        float t = 0f;
        while (explode_bomb.transform.position != targetposition)
        {
            t += Time.deltaTime * speed;
            explode_bomb.transform.position = Vector3.Lerp(startposition, targetposition, t);
            yield return null; // stop IEnumerator

        }

        if (status == 1)
        {
            bombTrigger.rightBomb();
        }
        else
        {
            bombTrigger.leftBomb();
        }
        ending_source.Play();
        bigBomb_voice.Play();
        shakeCamera.enabled = true;
    }

}



