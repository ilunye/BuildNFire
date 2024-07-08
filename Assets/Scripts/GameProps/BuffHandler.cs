using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{

    public LinkedList<BuffInfo> buffList = new LinkedList<BuffInfo>(); //存放buff的链表

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        BuffRemove();
    }

    private void AddBuff(BuffInfo buffInfo)  //捡到物品增加buff
    {
        //查找传入的buff并返回
        BuffInfo findBuffInfo = FindBuff(buffInfo.buffData.BuffID);

        if (findBuffInfo != null) //如果找到
        {
            buffInfo.curStack=1;
            switch (buffInfo.buffData.buffUpdateTime) //判断更新模式
            {
                case BuffUpdateTimeEnum.Add: //添加，时间叠加
                    findBuffInfo.durationTime += findBuffInfo.buffData.DurationTime;
                    break;
                case BuffUpdateTimeEnum.Replace: //替换，时间重置
                    findBuffInfo.durationTime = findBuffInfo.buffData.DurationTime;
                    break;
            }
            findBuffInfo.buffData.OnCreate.Apply(findBuffInfo); //启动buff
        }
        else //没找到
        {
            buffInfo.durationTime = buffInfo.buffData.DurationTime;
            buffInfo.buffData.OnCreate.Apply(buffInfo); //启动buff
            buffList.AddLast(buffInfo); //添加到buffList的末尾

            //根据priority对buffList进行排序
            SortBuffList(buffList);


        }


    }

    public void RemoveBuff(BuffInfo buffInfo) //去掉buff
    {
        switch (buffInfo.buffData.buffRemoveStackUpdate) //判断去掉模式
        {
            case BuffRemoveStackUpdateEnum.Clear: //清除效果
                buffInfo.buffData.OnRemove.Apply(buffInfo);
                buffList.Remove(buffInfo);
                break;

            case BuffRemoveStackUpdateEnum.Reduce: //减弱效果
                buffInfo.curStack--; //减少一层
                if (buffInfo.curStack == 0) //层数为0时清除
                {
                    buffInfo.buffData.OnRemove.Apply(buffInfo);
                    buffList.Remove(buffInfo);
                }
                else
                {
                    buffInfo.durationTime = buffInfo.buffData.DurationTime;

                }
                break;
        }
    }



    private void BuffRemove()
    {
        List<BuffInfo> DeleteBuffList = new List<BuffInfo>();
        foreach (var buffInfo in buffList) //遍历buffList
        {
            if (buffInfo.durationTime < 0) //生效时间已过,去掉buff
            {
                DeleteBuffList.Add(buffInfo);
            }
            else
            {   //未到生效时间，减去经过时间
                buffInfo.durationTime -= Time.deltaTime;
            }
        }
        foreach (var buffInfo in DeleteBuffList)
        {
            RemoveBuff(buffInfo);
        }
    }

    private void SortBuffList(LinkedList<BuffInfo> List) //插入排序
    {
        if (List == null || List.First == null)
        {
            return; //链表为空或只有一个元素时直接返回
        }
        LinkedListNode<BuffInfo> current = List.First.Next;
        while (current != null)
        {
            LinkedListNode<BuffInfo> next = current.Next;
            LinkedListNode<BuffInfo> prev = current.Previous;

            while (prev != null && prev.Value.buffData.Priority > current.Value.buffData.Priority)
            {
                prev = prev.Previous;
            }

            if (prev == null)
            {
                List.Remove(current);
                List.AddFirst(current)
        ;
            }
            else
            {
                List.Remove(current);
                List.AddAfter(prev, current);
            }

            current = next;
        }
        throw new NotImplementedException();
    }

    private BuffInfo FindBuff(int buffDataID) //在buff列表中查找buff编号
    {
        foreach (var buffInfo in buffList)
        {
            if (buffInfo.buffData.BuffID == buffDataID)
            {
                return buffInfo; //查找到返回
            }
        }
        return default; //未查找到返回默认值
    }
}
