using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectableMaterials : MonoBehaviour
{
    public Character.MaterialType materialType;
    // Start is called before the first frame update
    public bool WillDisappear = true;

    private bool claimed = false;
    void Awake()
    {
    }

    void Start()
    {
        StartCoroutine(DestroyAfterDelay(5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(WillDisappear)
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other){
        if(other.tag != "Player" || claimed)
            return;
        if(other.GetComponent<Character>().playerState == Character.PlayerState.Idle){
            other.GetComponent<Character>().playerState = Character.PlayerState.ReadyToClaim;
            if(other.GetComponent<Character>().Item == null){
                other.GetComponent<Character>().Item = gameObject;
            }
        }
        if(other.GetComponent<Character>().playerState == Character.PlayerState.Claim){
            if(other.GetComponent<Character>().Item != null){
                if(other.GetComponent<Character>().Item.name == gameObject.name){
                    claimed = true;
                    other.GetComponent<Character>().Material = materialType;
                    other.GetComponent<Character>().Item = gameObject;              // set the player's item as itself
                    Destroy(gameObject);
                }
            }
        }
    }
}
