using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class test : MonoBehaviour
{
    public float interval;
    public Vector3 spawnPosition;
    private int p;
    public float timer;
    private float throwForce;

    void Start()
    {
        float distanceAbove = 2f;
        spawnPosition=gameObject.transform.localPosition+transform.up*distanceAbove;
        interval=2.5f;
        p=(int)Random.Range(1,3);
        interval=interval*p;
        throwForce=1;
    }

     void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            GameObject c=Instantiate(Resources.Load("prefabs/Cube")as GameObject);
            Debug.Log("仍物体");
            c.transform.localPosition=spawnPosition;
            Rigidbody cubeRigidbody = c.GetComponent<Rigidbody>();
            cubeRigidbody.AddForce(Vector3.up*throwForce,ForceMode.Impulse);
            timer=0f;
            interval = 2.5f * Random.Range(1, 3);
            
        }
    }
}
