using System.Collections;
using UnityEngine;
using TMPro;

public class WorkFlow : MonoBehaviour
{
    public int wood_number=0;
    public int iron_number=0;
    public int gunpowder_number=0;
    public int projectile_number=0;
    
    public TMP_Text wood_text;
    public TMP_Text iron_text;
    public TMP_Text gunpowder_text;
    public TMP_Text projectile_text; 
    public GameObject frame_wood;
    public GameObject frame_iron;

    public static bool isIron;
    public static bool isWood;
    public static bool isPro;
    public static bool isPowder;

    private bool toPickWood;
    private bool toPickIron;

    void Start()
    {
        isIron = false;
        isWood = false;
        isPro = false;
        isPowder = false;
        toPickIron=false;
        toPickWood=false;
        AbleAllChildren(frame_iron);
        DisableAllChildren(frame_wood);

    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("toPickIron"+toPickIron+" ");
            if (toPickIron&& iron_number <= 6 && isIron)
            {
                Debug.Log("啊啊啊啊");
                iron_number++;
                toPickIron=!toPickIron;
                toPickWood=!toPickWood;
                DisableAllChildren(frame_iron);
                AbleAllChildren(frame_wood);
                isIron=false;
            }
            else if (toPickWood&& wood_number <= 4 && isWood)
            {
                wood_number++;
                toPickIron=!toPickIron;
                toPickWood=!toPickWood;
                AbleAllChildren(frame_iron);
                DisableAllChildren(frame_wood);
                isWood=false;
            }
            else if (isPro && projectile_number <= 1)
            {
                projectile_number++;
                isPro=false;
            }
            else if (isPowder && gunpowder_number <= 1)
            {
                gunpowder_number++;
                isPowder=false;
            }

            UpdateText();
        }
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

