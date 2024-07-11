using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGreenCar : MonoBehaviour
{
    private float interval;
    private Vector3 spawnPosition = new Vector3(799.874f, 0.2f, 991.6682f);
    private int p;
    public float timer;
    private float lifeTime=10f;

    void Start()
    {
        interval=4f;
        p=(int)Random.Range(1,3);
        interval=interval*p;
    }

     void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            Instantiate((GameObject)Resources.Load("prefabs/Vehicle_Container_color01_separate"), spawnPosition, rotation);
            timer=0f;
        }
    }
    
}
