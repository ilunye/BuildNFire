using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;

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
        }
        else if (status == 2 && !gameover)
        {
            theText.text = "PLAYER TWO WON!";
            button.SetActive(true);
            Play_Final();
            gameover = true;
        }
        if (gameover)
        {
            ending_source.Play();
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



