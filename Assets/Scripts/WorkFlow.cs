using System.Collections;
using UnityEngine;
using TMPro;

public class WorkFlow : MonoBehaviour
{
    public int wood_number=0;
    public int iron_number=0;
    public int gunpowder_number=0;
    public int projectile_number=0;
    
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
        HandleInput();
    }

    private void HandleInput()
    {
        if (toPickIron&& iron_number <= 6 && isIron)
        {
            iron_number++;
            if(iron_number <= 4){
                toPickIron=false;
                toPickWood=true;
                DisableAllChildren(frame_iron);
                AbleAllChildren(frame_wood);
            }else if(iron_number == 6){
                toPickIron=false;
                toPickWood=false;
                DisableAllChildren(frame_iron);
                DisableAllChildren(frame_wood);
            }
        }
        else if (toPickWood&& wood_number <= 4 && isWood)
        {
            wood_number++;
            toPickIron=true;
            toPickWood=false;
            AbleAllChildren(frame_iron);
            DisableAllChildren(frame_wood);
        }
        else if (isPro && projectile_number <= 1)
        {
            projectile_number++;
        }
        else if (isPowder && gunpowder_number <= 1)
        {
            gunpowder_number++;
        }
        isIron = false;
        isWood = false;
        isPro = false;
        isPowder = false;

        UpdateText();
    }

    private void UpdateText()
    {
        wood_text.text = wood_number + "/4";
        iron_text.text = iron_number + "/6";
        gunpowder_text.text = gunpowder_number + "/1";
        projectile_text.text = projectile_number + "/1";
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

