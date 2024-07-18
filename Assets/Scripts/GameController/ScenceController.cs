using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public TreeController treeController;
    public TrafficControl trafficControl;
    private float scaleTime = 2f;
    public static int sceneID = 0;


    void Start()
    {
        treeController = gameObject.GetComponent<TreeController>();
        trafficControl = GameObject.Find("Road").GetComponent<TrafficControl>();
        // 获取当前场景的名称
        string sceneName = SceneManager.GetActiveScene().name;

        // 根据场景名称执行不同的逻辑
        if (sceneID == 0) //vally
        {

            // 在场景1中执行特定的逻辑
            //Debug.Log("在场景1中");
        }
        else if (sceneID == 1) //city
        {
            if (treeController != null)
            {
                treeController.loadTrack = "Prefabs/BigTree";
            }
            if (trafficControl != null)
            {
                trafficControl.car1_load_track = "Prefabs/Vehicle_Bus";
                trafficControl.car2_load_track = "Prefabs/Vehicle_Truck";

            }

            // 在场景2中执行特定的逻辑
            //Debug.Log("在场景2中" + CarThrow.bigScale);
        }
        else //dark city
        {

            if (trafficControl != null)
            {

                trafficControl.car1_load_track = "Prefabs/red_car";
                trafficControl.car2_load_track = "Prefabs/green_car";

            }

        }
        void Update()
        {
            //StartCoroutine(ChangeScale());
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

        }



    }
}