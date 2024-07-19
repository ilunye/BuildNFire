using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class Cannon : MonoBehaviour
{
    public bool claimed = false;
    private bool isPlaying = false;
    private Material[] material = new Material[5];
    public Transform disOffset;
    private Transform down;
    private Transform up;
    private Transform middle;
    private float[] ckpts = new float[10];
    public int idx = 0;     // max: 10
    public bool isProtected = true;
    public GameObject player = null;
    public GameObject player2 = null;
    public bool playerIn = false;
    public bool playerIn2 = false;
    public WorkFlow workFlow;
    public GameStartTextController gameStartTextController;
    public int mode = 0;
    // these two only used in mode 1
    public bool enabled = false;
    private bool building = false;
    private float timer = 0f;
    public int robot_idx = 0;
    private float[] robot_ckpts = new float[150];
    public GameObject sum_msg;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerIn = true;
        }
        if(other.gameObject == player2)
        {
            playerIn2 = true;
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject == player){
            playerIn = true;
        }
        if(other.gameObject == player2){
            playerIn2 = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerIn = false;
        }
        if(other.gameObject == player2)
        {
            playerIn2 = false;
        }
    }

    IEnumerator each_next(float offset)
    {
        isPlaying = true;
        float eachOffset = offset / 50;
        for (int i = 0; i < 50; i++)
        {
            disOffset.position = new Vector3(disOffset.position.x, disOffset.position.y + eachOffset, disOffset.position.z);
            yield return new WaitForSeconds(0.1f);
        }
        isPlaying = false;
    }

    public void next_state(GameObject player)
    {
        if (isPlaying || idx == 10)
        {
            return;
        }
        player.GetComponent<Animator>().Play("CastingLoop");
        //Debug.Log("player gathering");
        player.GetComponent<Character>().playerState = Character.PlayerState.Operating;
        if (idx < 8)
        {
            StartCoroutine(each_next((middle.position.y - down.position.y) / 4));
        }
        else
        {

            StartCoroutine(each_next((up.position.y - middle.position.y) / 1));


        }
        idx += 2;
        if (idx >= 10)
            idx = 10;

    }

    public void prev_state()
    {
        if (idx > 0)
        {
            StopAllCoroutines();
            isPlaying = true;
            if (isProtected)
            {        // by default this branch
                idx = idx - 1 < 0 ? 0 : idx - 1;
            }
            else
            {
                idx = idx - 2 < 0 ? 0 : idx - 2;
            }
            disOffset.position = new Vector3(disOffset.position.x, ckpts[idx], disOffset.position.z);
            isPlaying = false;
        }
    }

    private void robot_build(){
        // 分成150个阶段完成，每个持续1s
        if(sum_msg.GetComponent<sum_msg>().status == 1)
            return;
        if(timer > 1f){
            timer = 0f;
            if(robot_idx < 149)
                robot_idx++;
            disOffset.position = new Vector3(disOffset.position.x, robot_ckpts[robot_idx], disOffset.position.z);
            for (int i = 0; i < 5; i++)
            {
                material[i].SetFloat("_DisappearOffset", disOffset.position.y);
            }
            if(robot_idx == 149){
                sum_msg.GetComponent<sum_msg>().status = 2;
            }
        }
    }

    void Start()
    {
        disOffset = gameObject.transform.GetChild(0);
        down = gameObject.transform.GetChild(1);
        up = gameObject.transform.GetChild(2);
        middle = gameObject.transform.GetChild(3);
        for (int i = 0; i < 5; i++)
        {
            material[i] = GetComponent<MeshRenderer>().materials[i];
            material[i].SetFloat("_DisappearOffset", disOffset.position.y);
        }
        for (int i = 0; i < 8; i++)
        {
            ckpts[i] = down.position.y + (middle.position.y - down.position.y) * i / 8;
        }
        for (int i = 8; i < 10; i++)
        {
            ckpts[i] = middle.position.y + (up.position.y - middle.position.y) * (i - 8) / 2;
        }
        for(int i = 0; i < 150; i++){
            robot_ckpts[i] = down.position.y + (up.position.y - down.position.y) * i / 150;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(player == null && player2 == null && mode == 1){                      // robot
            if(enabled){
                robot_build();
            }
            return;
        }
        for (int i = 0; i < 5; i++)
        {
            material[i].SetFloat("_DisappearOffset", disOffset.position.y);
        }
        bool playerdoing = false;
        if (playerIn && Input.GetKeyDown(player.GetComponent<Character>().keycodes[4]))
        {
            //Debug.Log("build");
            if (isPlaying) return;
            switch (player.GetComponent<Character>().Material)
            {
                case Character.MaterialType.CannonBall:
                    playerdoing = true;
                    workFlow.isPro = true;
                    if (workFlow.projectile_number < 1)
                    {
                        player.GetComponent<Character>().Material = Character.MaterialType.None;
                        player.GetComponent<Character>().Anim.Play("CastingLoop 2");
                        //Debug.Log("collect projectile");
                    }
                    break;
                case Character.MaterialType.GunPowder:
                    playerdoing = true;
                    workFlow.isPowder = true;
                    if (workFlow.gunpowder_number < 1)
                    {
                        player.GetComponent<Character>().Material = Character.MaterialType.None;
                        player.GetComponent<Character>().Anim.Play("CastingLoop 2");
                        //Debug.Log("collect gunpowder");
                    }
                    break;
                case Character.MaterialType.Iron:
                    playerdoing = true;
                    workFlow.isIron = true;
                    if (workFlow.iron_number < 3 && workFlow.toPickIron)
                    {
                        player.GetComponent<Character>().Material = Character.MaterialType.None;
                        next_state(player);
                    }
                    break;
                case Character.MaterialType.Wood:
                    playerdoing = true;
                    workFlow.isWood = true;
                    if (workFlow.wood_number < 2 && workFlow.toPickWood)
                    {
                        player.GetComponent<Character>().Material = Character.MaterialType.None;
                        next_state(player);
                    }
                    break;
            }
        }
        if((!playerdoing) && (mode != 0 && playerIn2 && Input.GetKeyDown(player2.GetComponent<Character>().keycodes[4]))){
            //Debug.Log("build");
            if (isPlaying) return;
            switch (player2.GetComponent<Character>().Material)
            {
                case Character.MaterialType.CannonBall:
                    workFlow.isPro = true;
                    if (workFlow.projectile_number < 1)
                    {
                        player2.GetComponent<Character>().Material = Character.MaterialType.None;
                        player2.GetComponent<Character>().Anim.Play("CastingLoop 2");
                        //Debug.Log("collect projectile");
                    }
                    break;
                case Character.MaterialType.GunPowder:
                    workFlow.isPowder = true;
                    if (workFlow.gunpowder_number < 1)
                    {
                        player2.GetComponent<Character>().Material = Character.MaterialType.None;
                        player2.GetComponent<Character>().Anim.Play("CastingLoop 2");
                        //Debug.Log("collect gunpowder");
                    }
                    break;
                case Character.MaterialType.Iron:
                    workFlow.isIron = true;
                    if (workFlow.iron_number < 3 && workFlow.toPickIron)
                    {
                        player2.GetComponent<Character>().Material = Character.MaterialType.None;
                        next_state(player2);
                    }
                    break;
                case Character.MaterialType.Wood:
                    workFlow.isWood = true;
                    if (workFlow.wood_number < 2 && workFlow.toPickWood)
                    {
                        player2.GetComponent<Character>().Material = Character.MaterialType.None;
                        next_state(player2);
                    }
                    break;
            }
        }

        
    }
}
