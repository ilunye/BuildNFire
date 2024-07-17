using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public TreeController treeController;
    public TrafficControl trafficControl;
    public float scaleTime = 2f;


    void Start()
    {
        treeController = gameObject.GetComponent<TreeController>();
        trafficControl = GameObject.Find("Road").GetComponent<TrafficControl>();
        // 获取当前场景的名称
        string sceneName = SceneManager.GetActiveScene().name;

        // 根据场景名称执行不同的逻辑
        if (sceneName == "Main")
        {
            // 在场景1中执行特定的逻辑
            //Debug.Log("在场景1中");
        }
        else if (sceneName == "City")
        {
            if (treeController != null)
            {
                treeController.regionleft_xmin = 170f;
                treeController.regionleft_xmax = 177f;
                treeController.regionright_xmin = 183f;
                treeController.regionright_xmax = 190f;
                treeController.region_zmin = 134f;
                treeController.region_zmax = 144f;
                treeController.loadTrack = "Prefabs/BigTree";
            }
            if (trafficControl != null)
            {
                trafficControl.car1_load_track = "Prefabs/Vehicle_Bus";
                trafficControl.car2_load_track = "Prefabs/Vehicle_Truck";

            }


            // 在场景2中执行特定的逻辑
            //Debug.Log("在场景2中");
        }
        else
        {
            // 在其他场景中执行默认逻辑
            //Debug.Log("在其他场景中");
        }


    }
    void Update()
    {
        //StartCoroutine(ChangeScale());
        Debug.Log("debug");
    }







    IEnumerator ChangeScale()
    {
        Debug.Log("Start of Coroutine");

        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject tempObj in allGameObjects)
        {

            if (tempObj.tag == "material" || tempObj.tag == "Bomb" || tempObj.tag == "Wood"
            || tempObj.tag == "Burger" || tempObj.tag == "Stone" || tempObj.tag == "Lock"
            || tempObj.tag == "Cannon" || tempObj.tag == "Clock" || tempObj.tag == "Box"
            || tempObj.tag == "Iron" || tempObj.tag == "Magnet")
            {
                tempObj.transform.localScale *= scaleTime;
                Debug.Log(tempObj.name + tempObj.transform.localScale);
            }
        }
        yield return null;

        Debug.Log("End of Coroutine");
    }



}