using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class TrafficControl : MonoBehaviour
{
    private float interval_green;
    private float interval_red;
    public float timer_green;
    public float timer_red;
    public String car1_load_track = "Prefabs/Vehicle_Container_color01_separate";
    public String car2_load_track = "Prefabs/Vehicle_Container_color03_separate";

    public float minTime = 4f;
    public float maxTime = 6f;

    void Start()
    {
        // car1_load_track = "Prefabs/Vehicle_Container_color01_separate";
        // car2_load_track = "Prefabs/Vehicle_Container_color03_separate";
        interval_green = UnityEngine.Random.Range(minTime, maxTime);
        interval_red = UnityEngine.Random.Range(minTime, maxTime);
    }

    void Update()
    {
        timer_green += Time.deltaTime;
        if (timer_green >= interval_green)
        {
            Instantiate(Resources.Load(car1_load_track));
            interval_green = UnityEngine.Random.Range(minTime, maxTime);
            timer_green = 0f;
        }
        timer_red += Time.deltaTime;
        if (timer_red >= interval_red)
        {
            Instantiate(Resources.Load(car2_load_track));
            interval_red = UnityEngine.Random.Range(minTime, maxTime);
            timer_red = 0f;
        }
    }

}