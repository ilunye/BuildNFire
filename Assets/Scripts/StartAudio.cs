using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudio : MonoBehaviour
{
    
    void Start()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();//从音频的第一秒开始播放音频
        audioSource.time = 1.2f;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
