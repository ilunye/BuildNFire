using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    private Animator animator;
    public Material material;
    public GameObject player;
    public GameObject player2 = null;
    public int mode = 0;
    private Transform outPos;
    private bool playerIn = false;
    private bool playerIn2 = false;
    private bool hasFire = false;
    private bool hasStone = false;
    public GameObject fire;
    public GameObject open_door;
    public GameObject clock;
    public static float bigScale = 1f;

    public string furnace_name = "f0";
    private int iron_number = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerIn = true;
        }
        if(other.gameObject == player2)
        {
            playerIn2 = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerIn = true;
        }
        if(other.gameObject == player2)
        {
            playerIn2 = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerIn = false;
        }
        if(other.gameObject == player2)
        {
            playerIn2 = false;
        }
    }
    public void Play()
    {
        animator.Play("furnace");
        //Debug.Log("play furnace animation" + animator);
    }
    private void Fireoff()
    {
        material.SetColor("_EmissionColor", Color.black);
        hasFire = false;
    }
    public void AddFire()
    {
        material.SetColor("_EmissionColor", Color.white);
        hasFire = true;
        Invoke("Fireoff", 15f);
    }

    public void ADDStone()
    {
        hasStone = true;
        Invoke("Stoneoff", 15f);
    }
    private void Stoneoff()
    {
        hasStone = false;
    }
    private void smelting()
    {
        GameObject g = Instantiate(Resources.Load("Prefabs/ConcreteTubes") as GameObject, outPos.position, Quaternion.identity);
        g.name = "concrete_tube_" + furnace_name + "_" + iron_number.ToString();
        g.transform.localScale *= bigScale;
        iron_number++;
        g.GetComponent<CollectableMaterials>().WillDisappear = false;
        Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        fire = GameObject.Find("fire");
        open_door = GameObject.Find("open_door");
        clock = GameObject.Find("clock1");

        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", Color.black);
        outPos = transform.GetChild(1);
        if (player == null)
        {
            Debug.LogWarning("player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool playerdone = false;
        if (playerIn && Input.GetKeyDown(player.GetComponent<Character>().keycodes[4]))
        {
            playerdone = true;
            //Debug.Log("enter");
            if (player.GetComponent<Character>().Material == Character.MaterialType.Wood)
            {
                //Debug.Log("open door");
                OpenDoor();
                player.GetComponent<Character>().Material = Character.MaterialType.None;
                Play();
                AddFire();
                clock1();
                AudioFire();
                if (hasStone)
                {
                    Invoke("smelting", 5f);
                }
                //Debug.Log(hasFire);

            }
            else if (player.GetComponent<Character>().Material == Character.MaterialType.IronOre)
            {
                OpenDoor();
                player.GetComponent<Character>().Material = Character.MaterialType.None;
                Play();
                ADDStone();
                clock1();
                if (hasFire)
                {
                    Invoke("smelting", 5f);
                }
            }
        }
        else if(playerIn2 && (!playerdone) && mode != 0 && Input.GetKeyDown(player2.GetComponent<Character>().keycodes[4])){
            if (player2.GetComponent<Character>().Material == Character.MaterialType.Wood)
            {
                OpenDoor();
                player2.GetComponent<Character>().Material = Character.MaterialType.None;
                Play();
                AddFire();
                clock1();
                AudioFire();
                if (hasStone)
                {
                    Invoke("smelting", 5f);
                }
            }
            else if (player2.GetComponent<Character>().Material == Character.MaterialType.IronOre)
            {
                OpenDoor();
                player2.GetComponent<Character>().Material = Character.MaterialType.None;
                Play();
                ADDStone();
                clock1();
                if (hasFire)
                {
                    Invoke("smelting", 5f);
                }
            }

        }
    }
    private void OpenDoor()
    {
        AudioSource opendoor = open_door.GetComponent<AudioSource>();//放音频
        float startTime = 0f;
        float duration = 2f;
        opendoor.time = startTime;
        opendoor.PlayScheduled(AudioSettings.dspTime);
        opendoor.SetScheduledEndTime(AudioSettings.dspTime + duration);


    }
    private void clock1()
    {
        AudioSource c = open_door.GetComponent<AudioSource>();
        float startTime = 0f;
        float duration = 15f;
        c.time = startTime;
        c.PlayScheduled(AudioSettings.dspTime);
        c.SetScheduledEndTime(AudioSettings.dspTime + duration);
    }
    private void AudioFire()
    {
        AudioSource c = fire.GetComponent<AudioSource>();
        float startTime = 0f;
        float duration = 15f;
        c.time = startTime;
        c.PlayScheduled(AudioSettings.dspTime);
        c.SetScheduledEndTime(AudioSettings.dspTime + duration);
    }

}
