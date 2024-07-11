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

    void OnTriggerStay(Collider other){
        if(other.tag != "Player" || claimed)
            return;
        if(other.GetComponent<Character>().playerState == Character.PlayerState.Idle)
            other.GetComponent<Character>().playerState = Character.PlayerState.ReadyToClaim;
        if(other.GetComponent<Character>().playerState == Character.PlayerState.Claim){
            claimed = true;
            other.GetComponent<Character>().Material = materialType;
            /*
            switch(materialType){
                case Character.MaterialType.Wood:
                    WorkFlow.isWood = true;
                    break;
                case Character.MaterialType.Iron:
                    WorkFlow.isIron = true;
                    break;
                case Character.MaterialType.GunPowder:
                    WorkFlow.isPowder = true;
                    break;
                case Character.MaterialType.CannonBall:
                    WorkFlow.isPro = true;
                    break;
            }
            */
            Destroy(gameObject);
        }
        
    }
}
