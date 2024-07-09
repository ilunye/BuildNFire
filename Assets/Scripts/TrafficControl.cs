using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TrafficControl : MonoBehaviour
{
    private float interval;
    private Vector3 spawnPosition = new Vector3(778.22f, 0.2f, 966.79f);
    private int p;
    public float timer;

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
            Instantiate(Resources.Load("prefabs/Vehicle_Container_color01_separate"), spawnPosition, Quaternion.identity);
            timer=0f;
        }
    }
    
}