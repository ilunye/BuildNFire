using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using Mirror;

public class NetTrafficControl : NetworkBehaviour
{
    private float interval_green;
    private float interval_red;
    public float timer_green;
    public float timer_red;
    public String car1_load_track = "Prefabs/Online/Vehicle_Container_color01_separate";
    public String car2_load_track = "Prefabs/Online/Vehicle_Container_color03_separate";

    public float minTime = 4f;
    public float maxTime = 6f;

    void Start()
    {
        // car1_load_track = "Prefabs/Vehicle_Container_color01_separate";
        // car2_load_track = "Prefabs/Vehicle_Container_color03_separate";
        if(!isServer) return;
        interval_green = UnityEngine.Random.Range(minTime, maxTime);
        interval_red = UnityEngine.Random.Range(minTime, maxTime);
    }

    void Update()
    {
        if(!isServer) return;
        timer_green += Time.deltaTime;
        if (timer_green >= interval_green)
        {
            GameObject g = Instantiate(Resources.Load(car1_load_track) as GameObject);
            NetworkServer.Spawn(g);
            interval_green = UnityEngine.Random.Range(minTime, maxTime);
            timer_green = 0f;
        }
        timer_red += Time.deltaTime;
        if (timer_red >= interval_red)
        {
            GameObject g = Instantiate(Resources.Load(car2_load_track) as GameObject);
            NetworkServer.Spawn(g);
            interval_red = UnityEngine.Random.Range(minTime, maxTime);
            timer_red = 0f;
        }
    }

}