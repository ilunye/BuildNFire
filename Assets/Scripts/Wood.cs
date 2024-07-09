using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        if(other.tag != "Player")
            return;
        Debug.Log("collide with wood!");
        if(other.GetComponent<Character>().playerState == Character.PlayerState.Idle)
            other.GetComponent<Character>().playerState = Character.PlayerState.ReadyToClaim;
        
    }
    
}
