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
        GameObject c;
        int r=Random.Range(0,4);//决定抛出物体
        if(r==0){
            c = Instantiate(Resources.Load("prefabs/Bomb Red") as GameObject);
        }
        else if(r==1){
            c = Instantiate(Resources.Load("prefabs/burger_1_lod0") as GameObject);
        }
        else if(r==2){
            c = Instantiate(Resources.Load("prefabs/Hourglass Green") as GameObject);
        }
        else{
            c = Instantiate(Resources.Load("prefabs/Lock Silver") as GameObject);
        }
        
        Debug.Log("扔物体");
        c.transform.localPosition = spawnPosition;
        Rigidbody cubeRigidbody = c.GetComponent<Rigidbody>();
        cubeRigidbody.AddForce(Vector3.up * throwForce, ForceMode.Impulse);

        StartCoroutine(RemoveRigidbodyAfterDelay(cubeRigidbody, 1.5f)); // 延时1.5秒后移除 Rigidbody

        timer = 0f;
        interval = 2.5f * Random.Range(1, 3);
    }
}

    IEnumerator RemoveRigidbodyAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (rb != null)
        {
            Destroy(rb); // 移除 Rigidbody 组件
        }
    }
}
