using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//挂在物品上
public class ObjectItem : MonoBehaviour  //物品类，相当于背包的一个格子
{
    public string objID;  //物品ID
    public string objName;  //物品名
    public int count = 0;  //物品数量
    public bool CanCombine = false; //判断是否可合并

    public bool IsCheck = false; //是否是待拾取的物品
    public bool IsGrab = false; //是否已经捡起来
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsCheck){ //如果是待拾取物品，变成红色
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
        IsCheck = false;
    }

    
}
