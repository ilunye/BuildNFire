//存储数据结构和枚举
using UnityEngine;

public enum BuffUpdateTimeEnum //buff更新方式
{
    Add, //效果叠加
    Replace, //效果重置
    Keep //效果不变

}

public enum BuffRemoveStackUpdateEnum
{
    Clear, //效果清除
    Reduce //效果减弱
}
public class BuffInfo
{
    public BuffData buffData;
    public GameObject creater; //buff创建者
    public GameObject target; //buff施加者
    public float durationTime; //持续时间
    public float tickTime;
    public int curStack;
}

public class DamageInfo
{
    public GameObject creater;
    public GameObject target;
    public float damage; //伤害值
}

[System.Serializable] //序列化，以便可以在unity窗口中显示
public class Property
{
    public float speed;
    public float sleep;
    public float attack;
}



