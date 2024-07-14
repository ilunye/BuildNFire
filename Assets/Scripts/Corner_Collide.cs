using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner_Collide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag != "Player")
            return;
        other.gameObject.GetComponent<Character>().InCorner= true;
    }
    void OnTriggerStay(Collider other){
        // Debug.Log("Stay");
        if(other.tag != "Player")
            return;
        other.gameObject.GetComponent<Character>().InCorner= true;
    }

    void OnTriggerExit(Collider other){
        // Debug.Log("Exit");
        if(other.tag != "Player")
            return;
        other.gameObject.GetComponent<Character>().InCorner= false;
    }
}
