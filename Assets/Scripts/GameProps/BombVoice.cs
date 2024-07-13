using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombVoice : MonoBehaviour
{
    public AudioClip soundEffect;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.clip = soundEffect;

    }

    void Update()
    {
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }
}
