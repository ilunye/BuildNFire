using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    private Animator animator;
    public Material material;
    public GameObject player;
    private Transform outPos;
    private bool playerIn = false;
    private bool hasFire = false;
    public GameObject fire;
    public GameObject open_door;
    public GameObject clock;

    public string furnace_name = "f0";
    private int iron_number = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerIn = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerIn = false;
        }
    }
    public void Play()
    {
        animator.Play("furnace");
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
    private void smelting()
    {
        GameObject g = Instantiate(Resources.Load("Prefabs/ConcreteTubes") as GameObject, outPos.position, Quaternion.identity);
        g.name = "concrete_tube_" + furnace_name + "_" + iron_number.ToString();
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
        Debug.Assert(player != null, "player is null");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIn && Input.GetKeyDown(player.GetComponent<Character>().keycodes[4]))
        {
            if (player.GetComponent<Character>().Material == Character.MaterialType.Wood)
            {
                OpenDoor();
                player.GetComponent<Character>().Material = Character.MaterialType.None;
                Play();
                AddFire();
                clock1();
                AudioFire();

            }
            else if (player.GetComponent<Character>().Material == Character.MaterialType.IronOre && hasFire)
            {
                OpenDoor();
                player.GetComponent<Character>().Material = Character.MaterialType.None;
                Play();
                Invoke("smelting", 5f);
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
