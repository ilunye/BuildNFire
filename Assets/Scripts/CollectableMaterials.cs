using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectableMaterials : MonoBehaviour
{
    public Character.MaterialType materialType;
    // Start is called before the first frame update

    private bool claimed = false;
    void Awake()
    {
    }

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnCollisionStay(Collision other){
    //     if(other.gameObject.tag != "Player" || claimed)
    //         return;
    //     if(other.gameObject.GetComponent<Character>().playerState == Character.PlayerState.Idle)
    //         other.gameObject.GetComponent<Character>().playerState = Character.PlayerState.ReadyToClaim;
    //     if(other.gameObject.GetComponent<Character>().playerState == Character.PlayerState.Claim){
    //         claimed = true;
    //         other.gameObject.GetComponent<Character>().Material = materialType;
    //         Destroy(gameObject);
    //     }
        
    // }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag != "Player" || claimed)
            return;
        if(other.gameObject.GetComponent<Character>().playerState == Character.PlayerState.Idle)
            other.gameObject.GetComponent<Character>().playerState = Character.PlayerState.ReadyToClaim;
        if(other.gameObject.GetComponent<Character>().playerState == Character.PlayerState.Claim){
            claimed = true;
            other.gameObject.GetComponent<Character>().Material = materialType;
            Destroy(gameObject);
        }
    }
}
