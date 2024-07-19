using System.Collections;
using UnityEngine;
using TMPro;
using System;
using Mirror;

public class NetWorkFlow : NetworkBehaviour
{
    [SyncVar]
    public float wood_number=0f;
    [Command(requiresAuthority = false)]
    public void CmdSetWood(float f)
    {
        wood_number = f;
    }
    [SyncVar]
    public float iron_number=0f;
    [Command(requiresAuthority = false)]
    public void CmdSetIron(float f)
    {
        iron_number = f;
    }
    [SyncVar]
    public float gunpowder_number=0f;
    [Command(requiresAuthority = false)]
    public void CmdSetGunpowder(float f)
    {
        gunpowder_number = f;
    }
    [SyncVar]
    public float projectile_number=0f;
    [Command(requiresAuthority = false)]
    public void CmdSetProjectile(float f)
    {
        projectile_number = f;
    }
    
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

    [SyncVar]
    public bool toPickWood;
    [Command(requiresAuthority = false)]
    public void CmdSetToPickWood(bool b)
    {
        toPickWood = b;
    }
    [SyncVar]
    public bool toPickIron;
    [Command(requiresAuthority = false)]
    public void CmdSetToPickIron(bool b)
    {
        toPickIron = b;
    }

/*
|--Wood--|--Iron--|--Wood--|--Iron--|--Iron--|--Iron--|
*/
    [SyncVar]
    public int workFlowPos = 0; // start at 0, max = 12
    [Command(requiresAuthority = false)]
    public void CmdSetWorkFlowPos(int i)
    {
        workFlowPos = i;
    }

    public bool last_is_iron = false;
    public bool last_is_wood = false;
    public int mode = 0;

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
                CmdSetWood(temp);
            }else
                CmdSetWood(temp + 0.5f);

            if(mod < 1)
                CmdSetIron(temp);
            else if(mod < 2)
                CmdSetIron(temp + 0.5f);
            else
                CmdSetIron(temp + 1.0f);
        }else{
            int temp = (workFlowPos - 8) / 2;
            int mod = (workFlowPos - 8) % 2;
            if(mod == 0)
                CmdSetIron(2.0f + temp);
            else
                CmdSetIron(2.0f + temp + 0.5f);
        }
        if(workFlowPos % 4 < 2 && workFlowPos < 8){
            CmdSetToPickWood(false);
            CmdSetToPickIron(true);
        }else if(workFlowPos % 4 >= 2 && workFlowPos < 8){
            CmdSetToPickWood(true);
            CmdSetToPickIron(false);
        }else if(workFlowPos < 10){
            CmdSetToPickWood(false);
            CmdSetToPickIron(true);
        }else{
            CmdSetToPickWood(false);
            CmdSetToPickIron(false);
        }

        if(last_is_iron)
            CmdSetWood((float)(Math.Floor(wood_number)));
        else if(last_is_wood)
            CmdSetIron((float)(Math.Floor(iron_number)));

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
            last_is_iron = true;
            last_is_wood = false;
            CmdSetWorkFlowPos(workFlowPos+2);
            if(workFlowPos > 10)
                CmdSetWorkFlowPos(10);
            iron_number += 1f;
            CmdSetIron((float)(iron_number + 1));
            if(iron_number > 3f)
                // iron_number = 3f;
                CmdSetIron(3f);
            if(iron_number <= 2f){
                CmdSetToPickIron(false);
                CmdSetToPickWood(true);
                DisableAllChildren(frame_iron);
                AbleAllChildren(frame_wood);
            }else if(iron_number < 3f){
                CmdSetToPickIron(true);
                CmdSetToPickWood(false);
                DisableAllChildren(frame_iron);
                DisableAllChildren(frame_wood);
            }else{
                CmdSetToPickIron(false);
                CmdSetToPickWood(false);
                DisableAllChildren(frame_iron);
                DisableAllChildren(frame_wood);
            }
        }else if(toPickWood && wood_number < 2f && isWood){
            last_is_wood = true;
            last_is_iron = false;
            CmdSetWorkFlowPos(workFlowPos+2);
            if(workFlowPos > 10)
                CmdSetWorkFlowPos(10);
            // wood_number += 1f;
            CmdSetWood(wood_number + 1f);
            if(wood_number > 2f)
                // wood_number = 2f;
                CmdSetWood(2f);
            CmdSetToPickIron(true);
            CmdSetToPickWood(false);
            AbleAllChildren(frame_iron);
            DisableAllChildren(frame_wood);
        }else if(isPro && projectile_number < 1f){
            CmdSetProjectile(projectile_number + 1f);
        }else if(isPowder && gunpowder_number < 1f){
            CmdSetGunpowder(gunpowder_number + 1f);
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

