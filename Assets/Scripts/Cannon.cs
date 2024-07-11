using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Cannon : MonoBehaviour
{
    public bool claimed = false;
    public bool isPlaying = false;
    private Material[] material = new Material[5];
    private Transform disOffset;
    private Transform down;
    private Transform up;
    private Transform middle;
    private float[] ckpts = new float[10];
    public int idx = 0;     // max: 10
    public bool isProtected = false;
    public GameObject player;
    private bool playerIn = false;
    private WorkFlow workFlow;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == player){
            playerIn = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject == player){
            playerIn = false;
        }
    }

    IEnumerator each_next(float offset){
        isPlaying = true;
        float eachOffset = offset / 50;
        for(int i=0; i<50; i++){
            disOffset.position = new Vector3(disOffset.position.x, disOffset.position.y + eachOffset, disOffset.position.z);
            yield return new WaitForSeconds(0.1f);
        }
        isPlaying = false;
    }

    public void next_state(){
        if(isPlaying || idx == 10){
            return;
        }
        Debug.Log("next_state");
        player.GetComponent<Animator>().Play("CastingLoop");
        player.GetComponent<Character>().playerState = Character.PlayerState.Operating;
        if(idx < 8){
            StartCoroutine(each_next((middle.position.y - down.position.y)/8));
        }else{
            StartCoroutine(each_next((up.position.y - middle.position.y)/2));
        }
        idx++;
    }

    public void prev_state(){
        if(idx > 0){
            StopAllCoroutines();
            isPlaying = false;
            if(isProtected){
                idx = idx-1 < 0 ? 0 : idx-1;
            }else{
                idx = idx-2 < 0 ? 0 : idx-2;
            }
            disOffset.position = new Vector3(disOffset.position.x, ckpts[idx], disOffset.position.z);
        }
    }

    void Start()
    {
        disOffset = gameObject.transform.GetChild(0);
        down = gameObject.transform.GetChild(1);
        up = gameObject.transform.GetChild(2);
        middle = gameObject.transform.GetChild(3);
        for(int i=0; i<5; i++){
            material[i] = GetComponent<MeshRenderer>().materials[i];
            material[i].SetFloat("_DisappearOffset", disOffset.position.y);
        }
        for(int i=0; i<8; i++){
            ckpts[i] = down.position.y + (middle.position.y - down.position.y) * i / 8;
        }
        for(int i=8; i<10; i++){
            ckpts[i] = middle.position.y + (up.position.y - middle.position.y) * (i-8) / 2;
        }
        workFlow = GameObject.Find("Canvas").GetComponent<WorkFlow>();
        Debug.Assert(player != null, "player is null");
    }

    void Update()
    {
        for(int i=0; i<5; i++){
            material[i].SetFloat("_DisappearOffset", disOffset.position.y);
        }
        if(playerIn && Input.GetKeyDown(KeyCode.E)){
            if(isPlaying) return;
            switch(player.GetComponent<Character>().Material){
                case Character.MaterialType.CannonBall:
                    WorkFlow.isPro = true;
                    player.GetComponent<Character>().Material = Character.MaterialType.None;
                    break;
                case Character.MaterialType.GunPowder:
                    WorkFlow.isPowder = true;
                    player.GetComponent<Character>().Material = Character.MaterialType.None;
                    break; 
                case Character.MaterialType.Iron:
                    WorkFlow.isIron = true;
                    if(workFlow.iron_number <= 6 && workFlow.toPickIron){
                        player.GetComponent<Character>().Material = Character.MaterialType.None;
                        next_state();
                    }
                    break;
                case Character.MaterialType.Wood:
                    WorkFlow.isWood = true;
                    if(workFlow.wood_number <= 4 && workFlow.toPickWood){
                        player.GetComponent<Character>().Material = Character.MaterialType.None;
                        next_state();
                    }
                    break;
            }
        }
    }
}
