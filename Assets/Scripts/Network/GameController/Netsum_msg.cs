using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Mirror;

public class Netsum_msg : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject button;
    [SyncVar]
    public int status = 0;  // 0: not end, 1: player 1 won, 2: player 2 won
    public TMP_Text theText = null;
    GameObject player1, player2;

    public GameObject explode_bomb;

    private bool gameover = false;

    private GameObject ending;
    private AudioSource ending_source;

    private GameObject bigBomb;
    private AudioSource bigBomb_voice;
    private NetBombTrigger bombTrigger;

    private ShakeCamera shakeCamera;

    private NetCannon cannon1;
    private NetCannon cannon2;
    public int scenceID = 1;

    public float changeScale = 1f;
    private int idxControl;
    private int idx1;
    private int idx2;
    public GameStartTextController gameStartTextController;
    public int mode = 0;

    [Command(requiresAuthority = false)]
    public void CmdSetStatus(int s)
    {
        status = s;
    }

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

        bombTrigger = GetComponent<NetBombTrigger>();
    }
    void Start()
    {
        button.SetActive(false);
        ending = Instantiate(Resources.Load("Audio/ending") as GameObject);
        ending_source = ending.GetComponent<AudioSource>();
        bigBomb = Instantiate(Resources.Load("Audio/bigbomb") as GameObject);
        bigBomb_voice = bigBomb.GetComponent<AudioSource>();
        shakeCamera = GameObject.Find("MainCamera").GetComponent<ShakeCamera>();
        cannon1 = GameObject.Find("cannon_1").GetComponent<NetCannon>();
        cannon2 = GameObject.Find("cannon_2").GetComponent<NetCannon>();


    }

    // Update is called once per frame
    void Update()
    {
        if(mode == 0){
            if (player1.GetComponent<WorkFlow>().iron_number == 3 && player1.GetComponent<WorkFlow>().wood_number == 2 && player1.GetComponent<WorkFlow>().gunpowder_number == 1 && player1.GetComponent<WorkFlow>().projectile_number == 1)
            {
                CmdSetStatus(1);
            }
            else if (player2.GetComponent<WorkFlow>().iron_number == 3 && player2.GetComponent<WorkFlow>().wood_number == 2 && player2.GetComponent<WorkFlow>().gunpowder_number == 1 && player2.GetComponent<WorkFlow>().projectile_number == 1)
            {
                CmdSetStatus(2);
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
            idx1 = cannon1.idx;
            idx2 = cannon2.idx;
            idxControl = Mathf.Max(idx1, idx2);

            if (idxControl < 5)
            {
                gameStartTextController.BGM_voice.pitch = 1f;
            }
            else if (idxControl >= 5 && idxControl < 7)
            {
                gameStartTextController.BGM_voice.pitch = 1.2f;
            }
            else if (idxControl >= 7 && idxControl < 9)
            {
                gameStartTextController.BGM_voice.pitch = 1.4f;
            }
            else
            {
                gameStartTextController.BGM_voice.pitch = 1.6f;
            }
        }else{
            if(player1.GetComponent<WorkFlow>().iron_number == 5 && player1.GetComponent<WorkFlow>().wood_number == 3 && player1.GetComponent<WorkFlow>().gunpowder_number == 2 && player1.GetComponent<WorkFlow>().projectile_number == 2)
                CmdSetStatus(1);
            if(status == 1 && !gameover){
                theText.text = "YOU WON!";
                button.SetActive(true);
                Play_Final();
                gameover = true;
            }else if(status == 2 && !gameover){
                theText.text = "YOU LOSE!";
                button.SetActive(true);
                Play_Final();
                gameover = true;
            }
            // win of robot is detected by cannon.cs
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
                    startposition = new Vector3(174f, 1.28f, 140.88f);
                    targetposition = new Vector3(187.13f, 1.28f, 140.88f);
                }
                else
                {

                    startposition = new Vector3(187.13f, 1.28f, 140.88f);
                    targetposition = new Vector3(174f, 1.28f, 140.88f);
                }
                break;
            default: //other scene
                if (status == 2)
                {
                    startposition = new Vector3(13.6f, 1.52731f, 5.2575f);
                    targetposition = new Vector3(13.9f, 1.515f, 17f);
                }
                else
                {

                    startposition = new Vector3(13.9f, 1.515f, 17f);
                    targetposition = new Vector3(13.6f, 1.52731f, 5.2575f);
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
        Destroy(explode_bomb);
    }

}



