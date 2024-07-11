using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombVoice : MonoBehaviour
{
    public AudioClip soundEffect;
    private AudioSource audioSource;
    public Bomb bomb;
    public GameObject bombobject;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundEffect;
        bombobject = GameObject.Find("Bomb Red");
        bomb = bombobject.GetComponent<Bomb>();
        Debug.Log("bomb:" + bomb);
    }

    void Update()
    {
        if (bomb.hasExploded)
        {
            audioSource.Play();
        }
    }
}
