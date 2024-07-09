using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TrafficControl : MonoBehaviour
{
    public float interval;
    public GameObject road;
    public Vector3 spawnPosition = new Vector3(20.32f, -3.9f, 7.24f);
    private int p;
    public float timer;

    void Start()
    {
        interval=3f;
        p=(int)Random.Range(1,3);
        interval=interval*p;
    }

     void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            GameObject car=Instantiate(Resources.Load("prefabs/Vehicle_Container_color01_separate")as GameObject);
            car.transform.SetParent(road.transform);
            car.transform.localPosition=spawnPosition;
            car.transform.localRotation=Quaternion.Euler(0,270f,0);
            timer=0f;
            if(car.transform.localPosition.x>25||car.transform.position.x<-35){
                Destroy(car);
            }
        }
    }
    
}