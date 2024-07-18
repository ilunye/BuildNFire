using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBombTarget : MonoBehaviour
{
    // Start is called before the first frame update
    private float InitthrowForce = 1f; // 初始投掷的力量

    private float throwDuration = 5f; // 投掷的持续时间
    private float elapsedTime = 0f; //计算投掷的时间

    public float throwForce; //投掷的力量

    private float MaxThrowForce = 5f; //投掷允許的最大力量
    public GameObject player;//玩家需要初始化
    public GameObject bomb;//炸弹
    Vector3 upVector = new Vector3(0f, 0.3f, 0f);
    private bool readytothrow = false;
    public bool target_dist_increasing = true;
    private float multipleFore = 3f;
    void Start()
    {
        throwForce=InitthrowForce;
    }

    // Update is called once per frame
    void Update()
    {
        //按E蓄力
        if(Input.GetKey(KeyCode.E)){
            if (!readytothrow)
                {      
                    readytothrow = true;
                    gameObject.transform.position = player.transform.position + player.transform.forward * 1f * throwForce + upVector;
                }
                else
                    gameObject.transform.position = player.transform.position + player.transform.forward * (1f * throwForce + 0.6f) + upVector;
                //Debug.Log(theBombTarget.transform.position);
                if (target_dist_increasing && throwForce < MaxThrowForce)
                {
                    throwForce += Time.deltaTime * multipleFore;
                    if(throwForce > MaxThrowForce)
                        throwForce = MaxThrowForce;
                    if(throwForce == MaxThrowForce)
                        target_dist_increasing = false;
                }
                else if(!target_dist_increasing && throwForce > InitthrowForce)
                {
                    throwForce -= Time.deltaTime * multipleFore;
                    if(throwForce < InitthrowForce)
                        throwForce = InitthrowForce;
                    if(throwForce == InitthrowForce)
                        target_dist_increasing = true;
                }
            if (Input.GetKeyUp(KeyCode.E)) // 检测玩家按下投掷按钮
            {
                readytothrow = false;

            }
        }
        
        if (bomb == null)
        {
            Destroy(gameObject); // 如果炸弹被销毁，销毁当前对象
        }
    }
    public void SetTarget(GameObject target)///不理解，还是不理解
{
    if (target == null)
    {
        Destroy(gameObject);
        readytothrow = false;
    }
    else
    {
        transform.position = target.transform.position;/////？？？？？？
        transform.rotation = target.transform.rotation;/////？？？？？？
        transform.localScale = target.transform.localScale;
    }
}

    public void SetReadyToThrow(bool ready)
    {
        readytothrow = ready;
    }

    public void ResetThrowForce()
    {
        throwForce = InitthrowForce;
    }
}
