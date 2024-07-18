/*using UnityEngine;

public class TrafficControlDarkCity : MonoBehaviour
{
    private float intervalGreen;
    private float intervalRed;
    public float timerGreen;
    public float timerRed;
    public float minTime = 4f;
    public float maxTime = 6f;

    void Start()
    {
        intervalGreen = Random.Range(minTime, maxTime);
        intervalRed = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        timerGreen += Time.deltaTime;
        if (timerGreen >= intervalGreen)
        {
            GameObject a=Instantiate(Resources.Load("Prefabs/green_car") as GameObject);
            intervalGreen = Random.Range(minTime, maxTime);
            timerGreen = 0f;
        }
        
        timerRed += Time.deltaTime;
        if (timerRed >= intervalRed)
        {
            GameObject a=Instantiate(Resources.Load("Prefabs/red_car") as GameObject);
            intervalRed = Random.Range(minTime, maxTime);
            timerRed = 0f;
        }
    }
}*/
