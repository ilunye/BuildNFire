using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//挂在玩家上
public class Pack : MonoBehaviour  //背包
{
    public List<ObjectItem> Items = null; //背包列表
    public int MaxItem = 32;  //每个物品的最大容量
    public int CurrentItemCount = 0; //背包中含有的物品种类数
    
    public Character player;
    
    // Start is called before the first frame update
    void Start()
    {
        Items = new List<ObjectItem>();  //初始化背包列表
        player = GetComponent<Character>(); //得到角色
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     public void GetItem(ObjectItem item)  //玩家拾捡物品
    {
        if(!item.CanCombine) //如果是新物品不能合并
        {
            if(Items.Count < MaxItem) //不超过背包容量，加入新物品
            {
                Items.Add(item); //加入列表
                item.count = 0;
            }
            else{
                Debug.Log("背包已经满啦！！");
            }
            
        }
        else{  //如果可以合并
            if(Items.Count < 1) //空背包直接添加
            {
                Items.Add(item);
                item.count = 0;
            }
            else{ //遍历背包合并物品
                foreach(ObjectItem currItem in Items)
                {
                    //相同物品，可叠加
                    if(currItem.objID == item.objID)
                    {
                        currItem.count += item.count;
                        item.count = 0;
                    }
                }

            }


        }
    }

    
    public void ShowPack() //展示背包物品
    {
        if(Input.GetKeyDown(KeyCode.K)) //按下K展示背包
        {
            int i = 1;
            string show = "背包：\n";
            foreach(ObjectItem currItem in Items)
            {
                show += i++ + "物品：[" + currItem.objName + "], 数量：" + currItem.count + "\n";
            }
            Debug.Log(show);
        }
        
    }
}

