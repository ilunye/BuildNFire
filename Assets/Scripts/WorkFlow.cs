using System.Collections;
using UnityEngine;
using TMPro;

public class WorkFlow : MonoBehaviour
{
    public float wood_number=0f;
    public float iron_number=0f;
    public float gunpowder_number=0f;
    public float projectile_number=0f;
    
    private TMP_Text wood_text;
    private TMP_Text iron_text;
    private TMP_Text gunpowder_text;
    private TMP_Text projectile_text; 
    private GameObject frame_wood;
    private GameObject frame_iron;

    public bool isIron;
    public bool isWood;
    public bool isPro;
    public bool isPowder;

    public bool toPickWood;
    public bool toPickIron;

/*
|--Wood--|--Iron--|--Wood--|--Iron--|--Iron--|--Iron--|
*/
    public int workFlowPos = 0; // start at 0, max = 12


    void Start()
    {
        isIron = false;
        isWood = false;
        isPro = false;
        isPowder = false;
        toPickIron=true;
        toPickWood=false;
        wood_text = GameObject.Find("Canvas/"+gameObject.name+"/wood/woodText").GetComponent<TMP_Text>();
        iron_text = GameObject.Find("Canvas/"+gameObject.name+"/iron/ironText").GetComponent<TMP_Text>();
        gunpowder_text = GameObject.Find("Canvas/"+gameObject.name+"/gunPowder/gunPowderText").GetComponent<TMP_Text>();
        projectile_text = GameObject.Find("Canvas/"+gameObject.name+"/cannonBall/cannonBallText").GetComponent<TMP_Text>();
        frame_wood = GameObject.Find("Canvas/"+gameObject.name+"/wood_frame");
        frame_iron = GameObject.Find("Canvas/"+gameObject.name+"/iron_frame");
        AbleAllChildren(frame_iron);
        DisableAllChildren(frame_wood);

    }

    void Update()
    {
        if(workFlowPos <= 8){
            int temp = workFlowPos / 4;
            int mod = workFlowPos % 4;
            if(mod <= 2){
                wood_number = temp * 1.0f;
            }else
                wood_number = temp + 0.5f;

            if(mod < 1)
                iron_number = temp * 1.0f;
            else if(mod < 2)
                iron_number = temp + 0.5f;
            else
                iron_number = temp + 1.0f;
        }else{
            int temp = (workFlowPos - 8) / 2;
            int mod = (workFlowPos - 8) % 2;
            if(mod == 0)
                iron_number = 2.0f + temp * 1.0f;
            else
                iron_number = 2.0f + temp + 0.5f;
        }
        if(workFlowPos % 4 < 2 && workFlowPos < 8){
            toPickWood = false;
            toPickIron = true;
        }else if(workFlowPos % 4 >= 2 && workFlowPos < 8){
            toPickWood = true;
            toPickIron = false;
        }else if(workFlowPos < 10){
            toPickWood = false;
            toPickIron = true;
        }else{
            toPickWood = false;
            toPickIron = false;
        }
        HandleInput();
    }

    private void HandleInput(){
        if(toPickIron){
            AbleAllChildren(frame_iron);
            DisableAllChildren(frame_wood);
        }
        if(toPickWood){
            AbleAllChildren(frame_wood);
            DisableAllChildren(frame_iron);
        }

        if(toPickIron && isIron && iron_number < 3f){
            Debug.Log("want iron come iron");
            workFlowPos += 2;
            if(workFlowPos > 10)
                workFlowPos = 10;
            iron_number += 1f;
            if(iron_number > 3f)
                iron_number = 3f;
            if(iron_number <= 2f){
                toPickIron = false;
                toPickWood = true;
                DisableAllChildren(frame_iron);
                AbleAllChildren(frame_wood);
            }else if(iron_number < 3f){
                toPickIron = true;
                toPickWood = false;
                DisableAllChildren(frame_iron);
                DisableAllChildren(frame_wood);
            }else{
                toPickIron = false;
                toPickWood = false;
                DisableAllChildren(frame_iron);
                DisableAllChildren(frame_wood);
            }
        }else if(toPickWood && wood_number < 2f && isWood){
            Debug.Log("want wood come wood");
            workFlowPos += 2;
            if(workFlowPos > 10)
                workFlowPos = 10;
            wood_number += 1f;
            if(wood_number > 2f)
                wood_number = 2f;
            toPickIron = true;
            toPickWood = false;
            AbleAllChildren(frame_iron);
            DisableAllChildren(frame_wood);
        }else if(isPro && projectile_number < 1f){
            projectile_number += 1f;
        }else if(isPowder && gunpowder_number < 1f){
            gunpowder_number += 1f;
        }

        isIron = false;
        isWood = false;
        isPro = false;
        isPowder = false;

        UpdateText();
    }
    // private void HandleInput()
    // {
    //     if (toPickIron&& iron_number < 6 && isIron)
    //     {
    //         iron_number++;
    //         if(iron_number <= 4){
    //             toPickIron=false;
    //             toPickWood=true;
    //             DisableAllChildren(frame_iron);
    //             AbleAllChildren(frame_wood);
    //         }else if(iron_number == 6){
    //             toPickIron=false;
    //             toPickWood=false;
    //             DisableAllChildren(frame_iron);
    //             DisableAllChildren(frame_wood);
    //         }
    //     }
    //     else if (toPickWood&& wood_number < 4 && isWood)
    //     {
    //         wood_number++;
    //         toPickIron=true;
    //         toPickWood=false;
    //         AbleAllChildren(frame_iron);
    //         DisableAllChildren(frame_wood);
    //     }
    //     else if (isPro && projectile_number < 1)
    //     {
    //         projectile_number++;
    //     }
    //     else if (isPowder && gunpowder_number < 1)
    //     {
    //         gunpowder_number++;
    //     }
    //     isIron = false;
    //     isWood = false;
    //     isPro = false;
    //     isPowder = false;

    //     UpdateText();
    // }

    private void UpdateText()
    {
        wood_text.text = wood_number.ToString() + "/2";
        iron_text.text = iron_number.ToString() + "/3";
        gunpowder_text.text = gunpowder_number.ToString() + "/1";
        projectile_text.text = projectile_number.ToString() + "/1";
    }

    void DisableAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(false); // 将子物体设为不可见
        }
    }
    void AbleAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true); // 将子物体设为可见
        }
    }
}

