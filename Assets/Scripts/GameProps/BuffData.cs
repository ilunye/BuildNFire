using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "_BuffData", menuName = "BuffSystem/BuffData", order = 1)]
public class BuffData : ScriptableObject
{
    //基本信息
    public int BuffID;
    public string BuffName;
    public Mesh icon; //buff皮肤
    public int MaxStack = 1; //可叠加层数
    public int Priority; //优先级
    public string[] tags;
    
    //时间信息
    public bool IsForever = false;
    public float DurationTime; //持续时间
    public float TickTime;

    //回掉点
    public BaseBuffModule OnCreate;
    public BaseBuffModule OnRemove;
    public BaseBuffModule OnTrick;
    public BaseBuffModule OnHit;
    public BaseBuffModule OnBeHit;

    //更新方式
    public BuffUpdateTimeEnum buffUpdateTime;
    public BuffRemoveStackUpdateEnum buffRemoveStackUpdate;
    
    private static BuffData instance;

    public static BuffData GetInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<BuffData>("YourBuffDataAssetName");
        }
        return instance;
    }
}

