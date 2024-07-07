using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float PlayerSpeed = 1f; //人物移动速度
    
    GameObject Player = null;
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()  //键盘控制玩家上下左右移动
    {
        float vertical = Input.GetAxis("Vertical");  //W S 上 下
        float horizontal = Input.GetAxis("Horizontal");  //A D 左 右
        
        Player.transform.Translate(Vector3.forward * vertical * PlayerSpeed * Time.deltaTime);//W S 上 下
        Player.transform.Translate(Vector3.right * horizontal * PlayerSpeed * Time.deltaTime);//A D 左右
    }

    private void GrabMaterial()  //玩家拾捡物品
    {
        if()
    }
}
