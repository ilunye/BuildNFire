using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sum_msg;
    public bool enabled = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 绕一个点做圆周运动
        if(enabled)
            transform.RotateAround(new Vector3(13.69f,0f,6.42f), transform.up, 50f * Time.deltaTime);
    }
}
