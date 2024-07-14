using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudio : MonoBehaviour
{
    
    void Start()
{
    //Invoke("PlayAudio", 1f);
    PlayAudio();
}

void PlayAudio()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
    
        audioSource.Play();
    }
}
