using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//挂在玩家上
public class Character : MonoBehaviour
{
    public float PlayerSpeed = 1f; //人物移动速度
    GameObject Player = null; //人物

    public Pack pack; //引用背包

    private Transform tr; //创造射线

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        pack = GetComponent<Pack>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove(); //人物移动
        pack.ShowPack(); //按下K展示背包
        RayCaseObj();  //拾捡物品

    }

    private void PlayerMove()  //键盘控制玩家上下左右移动
    {
        float vertical = Input.GetAxis("Vertical");  //W S 上 下
        float horizontal = Input.GetAxis("Horizontal");  //A D 左 右
        
        Player.transform.Translate(Vector3.forward * vertical * PlayerSpeed * Time.deltaTime);//W S 上 下
        Player.transform.Translate(Vector3.right * horizontal * PlayerSpeed * Time.deltaTime);//A D 左右
    }

    private void RayCaseObj()  //射线检测面前物品并捡起
    {
        //创建射线
        Debug.DrawRay(tr.position, tr.forward * 2.0f, Color.green);
        RaycastHit hit;
        //如果碰撞
        if(Physics.Raycast(tr.position, tr.forward, out hit, 2.0f))
        //将射线碰撞信息存储在 hit 变量中
        {
            Debug.Log ("射线击中:" + hit.collider.gameObject.name + "\n tag:" + hit.collider.tag);
            GameObject gameObj = hit.collider.gameObject; //获取碰到的物品
            ObjectItem obj =(ObjectItem)gameObj.GetComponent<ObjectItem>();
            if(obj != null)
            {
                Debug.Log("捡到的物品" + obj.name);
                obj.IsCheck = true;
                if(Input.GetKeyDown(KeyCode.E)) //按下E捡东西
                {
                    Debug.Log("按下E");
                    pack.GetItem(obj);
                    if(obj.IsGrab == true) //当前物品拾捡完成
                    {
                        Destroy(gameObj);
                    }
                }
            }
        }
    }
   
}
