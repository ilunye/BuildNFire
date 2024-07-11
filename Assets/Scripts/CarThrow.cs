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
    private float destroyDelay;


    void Start()
    {
        float distanceAbove = 2f;
        destroyDelay=5f;
        spawnPosition=gameObject.transform.localPosition+transform.up*distanceAbove;
        interval=1.5f;
        p=(int)Random.Range(1,3);
        interval=interval*p;
        throwForce=1;
        destroyDelay=5f;
    }

     void Update()
{
    timer += Time.deltaTime;
    if (timer >= interval)
    {
        GameObject c;
        int r=Random.Range(0,8);//决定抛出物体
        if(r==0){
            c = Instantiate(Resources.Load("prefabs/Bomb Red") as GameObject);
        }
        else if(r==1){
            c = Instantiate(Resources.Load("prefabs/burger_1_lod0") as GameObject);
        }
        else if(r==2){
            c = Instantiate(Resources.Load("prefabs/Hourglass Green") as GameObject);
        }
        else if(r==3){
            c = Instantiate(Resources.Load("prefabs/ConcreteTubes") as GameObject);
        }
        else if(r==4){
            c = Instantiate(Resources.Load("prefabs/projectile") as GameObject);
        }
        else if(r==5){
            c = Instantiate(Resources.Load("prefabs/Rock_03") as GameObject);
        }
        else if(r==6){
            c = Instantiate(Resources.Load("prefabs/Wood") as GameObject);
        }
        else{
            c = Instantiate(Resources.Load("prefabs/Lock Silver") as GameObject);
        }
        spawnPosition = transform.position + transform.up * 0.5f-transform.forward*0.5f;
        c.transform.localPosition=spawnPosition;
        Rigidbody cubeRigidbody = c.AddComponent<Rigidbody>();
        float x=Random.Range(-1.5f,1.5f);
        float z=Random.Range(-1.5f,1.5f);
        cubeRigidbody.AddForce(Vector3.up*throwForce+new Vector3(x,0,z),ForceMode.Impulse);
        if(r==1||r==2||r==3){
            StartCoroutine(RemoveRigidbodyAfterDelay(cubeRigidbody, 1.5f)); // 延时1.5秒后移除 Rigidbody
            StartCoroutine(BlinkAndDestroy(c, destroyDelay));
        }

        timer = 0f;
        interval = 1.5f * Random.Range(1, 3);
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
    IEnumerator BlinkAndDestroy(GameObject obj, float destroyDelay)
    {
        Debug.Log("闪烁");
        yield return new WaitForSeconds(destroyDelay);
    
        Destroy(obj);
    }
}
