using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Mirror;

public class NetCollectableMaterials : NetworkBehaviour
{
    public NetCharacter.MaterialType materialType;
    // Start is called before the first frame update
    public bool WillDisappear = true;

    private bool claimed = false;
    [Command(requiresAuthority = false)]
    public void CmdCollectableDestroy(GameObject obj){
        NetworkServer.Destroy(obj);
    }
    void Awake()
    {
    }

    void Start()
    {
        StartCoroutine(DestroyAfterDelay(12f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(WillDisappear && isServer)
            NetworkServer.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other){
        if(other.tag != "Player" || claimed)
            return;
        if(other.GetComponent<NetCharacter>().playerState == NetCharacter.PlayerState.Idle){
            other.GetComponent<NetCharacter>().playerState = NetCharacter.PlayerState.ReadyToClaim;
            if(other.GetComponent<NetCharacter>().Item == null){
                other.GetComponent<NetCharacter>().Item = gameObject;
            }
        }
        if(other.GetComponent<NetCharacter>().playerState == NetCharacter.PlayerState.Claim){
            if(other.GetComponent<NetCharacter>().Item != null){
                if(other.GetComponent<NetCharacter>().Item.name == gameObject.name){
                    claimed = true;
                    other.GetComponent<NetCharacter>().CmdSetMaterial(materialType);
                    other.GetComponent<NetCharacter>().Item = gameObject;              // set the player's item as itself
                    CmdCollectableDestroy(gameObject);
                }
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag != "Player" || claimed)
            return;
        if(other.GetComponent<NetCharacter>().playerState == NetCharacter.PlayerState.ReadyToClaim){
            other.GetComponent<NetCharacter>().playerState = NetCharacter.PlayerState.Idle;
        }
        if(other.GetComponent<NetCharacter>().Item != null && other.GetComponent<NetCharacter>().Item.name == gameObject.name){
            other.GetComponent<NetCharacter>().Item = null;
        }
    }
}
