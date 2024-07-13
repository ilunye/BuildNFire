using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TrafficControl : MonoBehaviour
{
    private float interval_green;
    private float interval_red;
    private Vector3 spawnPosition = new Vector3(778.22f, 0.0f, 966.79f);
    public float timer_green;
    public float timer_red;

    void Start()
    {
        interval_green=Random.Range(4,6);
        interval_red=Random.Range(4,6);
    }

     void Update()
    {
        timer_green += Time.deltaTime;
        if (timer_green >= interval_green)
        {
            Instantiate(Resources.Load("prefabs/Vehicle_Container_color01_separate"));
            interval_green=Random.Range(4,6);
            timer_green=0f;
        }
        timer_red+=Time.deltaTime;
        if (timer_red >= interval_red)
        {
            Instantiate(Resources.Load("prefabs/Vehicle_Container_color03_separate"));
            interval_green=Random.Range(4,6);
            timer_red=0f;
        }
    }
    
}