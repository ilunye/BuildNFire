using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Mono.CecilX;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{

    public LinkedList<BuffInfo> buffList = new LinkedList<BuffInfo>(); //存放buff的链表
    public BuffInfo CurrBuffInfo; //现在生效的buff

    public Bomb bomb;
    public GameObject CollObj;
    public ThrowBomb throwBomb;

    public BuffInfo bombbuffinfo;
    public GameObject eat_burger;
    public AudioSource eat_burger_source;

    public GameObject dejavu;
    public AudioSource dejavu_source;
    // Start is called before the first frame update
    void Start()
    {
        throwBomb = gameObject.GetComponent<ThrowBomb>();
        eat_burger = Instantiate(Resources.Load("Audio/eat_burger") as GameObject);
        eat_burger_source = eat_burger.GetComponent<AudioSource>();
        dejavu = Instantiate(Resources.Load("Audio/dejavu") as GameObject);
        dejavu_source = dejavu.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        BuffRemove(); //检测buff状态并更新

    }

    public void GetCollisionBuff(BuffInfo buffInfo, GameObject CollObj) //得到碰撞物体的buff信息
    {
        if (CollObj.tag == "Lock" || CollObj.tag == "Clock") //debug add to other
        {
            buffInfo.creater = gameObject;
            if (gameObject.name == "animal_people_wolf_1")
            {
                buffInfo.target = GameObject.Find("animal_people_wolf_2");
            }
            else
            {
                buffInfo.target = GameObject.Find("animal_people_wolf_1");
            }
        }
        else
        {
            buffInfo.creater = CollObj;
            buffInfo.target = gameObject;

        }

        buffInfo.durationTime = buffInfo.buffData.DurationTime;
        if (buffInfo != null)
        {
            AddBuff(buffInfo);
            if (CollObj.tag == "Lock") // add blue color
            {
                if (gameObject.name == "animal_people_wolf_1")
                    StartCoroutine(ChangeColorCoroutine(GameObject.Find("animal_people_wolf2")));
                else
                    StartCoroutine(ChangeColorCoroutine(GameObject.Find("animal_people_wolf1")));
            }
            Debug.Log("加上buff与" + CollObj.name);
            Destroy(CollObj);
        }
    }


    IEnumerator ChangeColorCoroutine(GameObject target1)
    {

        SkinnedMeshRenderer wolfRenderer = target1.GetComponent<SkinnedMeshRenderer>();

        Material[] materials = wolfRenderer.sharedMaterials;

        // 遍历所有的材质
        foreach (Material material in materials)
        {
            // 在这里可以对每个材质做进一步的处理
            material.color = new Color(65 / 255f, 105 / 255f, 225 / 255f);
        }



        yield return new WaitForSeconds(7f);
        foreach (Material material in materials)
        {
            // 在这里可以对每个材质做进一步的处理
            material.color = Color.white;
        }

    }




    private void OnTriggerStay(Collider collision)
    {
        //Debug.Log("进入trigger区" + collision.name);
        CollObj = collision.gameObject;
        if (CollObj != null)
        {
            BuffInfo buffInfo = new BuffInfo();
            switch (CollObj.tag) // 检测碰撞对象的标签
            {
                case "Burger":
                    eat_burger_source.Play();
                    dejavu_source.Play();
                    buffInfo.buffData = CollObj.GetComponent<Burger>().buffData;
                    Debug.Log("burger");
                    GetCollisionBuff(buffInfo, CollObj);
                    break;
                case "Clock":
                    buffInfo.buffData = CollObj.GetComponent<Clock>().buffData;
                    GetCollisionBuff(buffInfo, CollObj);
                    break;
                case "Lock":
                    buffInfo.buffData = CollObj.GetComponent<Lock>().buffData;

                    GetCollisionBuff(buffInfo, CollObj);

                    break;
                case "Bomb":

                    bomb = CollObj.GetComponent<Bomb>();
                    buffInfo.buffData = CollObj.GetComponent<Bomb>().buffData;
                    //Debug.Log("是否已经爆炸" + bomb.hasExploded );
                    //if (bomb.hasExploded == true) //爆炸了再施加debuff效果
                    //不能这样判断，因为爆炸后trigger就消失了
                    //Debug.Log("爆炸剩余时间还有：" + bomb.countdown);
                    if (bomb.countdown <= 0.1f)
                    {
                        buffInfo.creater = CollObj;
                        buffInfo.target = gameObject;
                        buffInfo.durationTime = buffInfo.buffData.DurationTime;
                        if (buffInfo != null)
                        {
                            AddBuff(buffInfo);
                            //Debug.Log("碰撞发生与" + CollObj.name);
                        }
                        bomb.Explode();
                    }

                    break;
                default:
                    break;

            }

        }
    }
    /*
    private void OnCollisionEnter(Collision collision) //检测碰撞
    {
        Debug.Log("Collide!");
        GameObject CollObj = collision.gameObject;
        if (CollObj != null)
        {
            BuffInfo buffInfo = new BuffInfo();
            switch (CollObj.tag) // 检测碰撞对象的标签
            {
                case "Burger":
                    buffInfo.buffData = CollObj.GetComponent<Burger>().buffData;
                    GetCollisionBuff(buffInfo, CollObj);
                    break;
                case "Clock":
                    buffInfo.buffData = CollObj.GetComponent<Clock>().buffData;
                    GetCollisionBuff(buffInfo, CollObj);
                    break;
                case "Lock":
                    buffInfo.buffData = CollObj.GetComponent<Lock>().buffData;
                    GetCollisionBuff(buffInfo, CollObj);
                    break;
                case "Bomb":
                    buffInfo.buffData = CollObj.GetComponent<Bomb>().buffData;
                    buffInfo.creater = CollObj;
                    buffInfo.target = gameObject;
                    buffInfo.durationTime = buffInfo.buffData.DurationTime;
                    if (buffInfo != null)
                    {
                        AddBuff(buffInfo);
                        Debug.Log("碰撞发生与" + CollObj.name);
                        Destroy(CollObj);
                    }
                    break;
                default:
                    Debug.Log("道具不存在");
                    break;



            }
        }
    }*/

    public void AddBuff(BuffInfo buffInfo)  //捡到物品增加buff
    {
        //查找传入的buff并返回
        BuffInfo findBuffInfo = FindBuff(buffInfo.buffData.BuffID);
        /*
                if (findBuffInfo != null) //如果找到
                {
                    buffInfo.curStack = 1;
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
                {*/
        foreach (var TempbuffInfo in buffList)
        {
            TempbuffInfo.durationTime = 0;
            Debug.Log("tempbuffinfo" + TempbuffInfo.durationTime);
        }
        buffInfo.durationTime = buffInfo.buffData.DurationTime;
        Debug.Log("启动buff" + buffInfo.buffData.BuffName);
        buffInfo.buffData.OnCreate.Apply(buffInfo); //启动buff

        buffList.AddLast(buffInfo); //添加到buffList的末尾

        //根据priority对buffList进行排序
        SortBuffList(buffList);



        /*
        Debug.Log(buffList);
    List<BuffInfo> DeleteBuffList = new List<BuffInfo>();
    if (buffList != null)
    {
    foreach (var tempbuffInfo in buffList)
    {
    Debug.Log("在deletebufflist中添加" + tempbuffInfo.buffData.BuffName);
        DeleteBuffList.Add(buffInfo);

    }
    }

    foreach (var tempbuffInfo in DeleteBuffList)
    {
    Debug.Log("遍历去掉buff" + tempbuffInfo.buffData.BuffName);
    RemoveBuff(tempbuffInfo);

    }
    buffInfo.durationTime = buffInfo.buffData.DurationTime;

    buffList.AddLast(buffInfo);
    Debug.Log("在bufflist中添加" + buffInfo.buffData.BuffName);
    Debug.Log("执行oncreate");
    buffInfo.buffData.OnCreate.Apply(buffInfo);*/


    }

    public void RemoveBuff(BuffInfo buffInfo) //去掉buff
    {
        switch (buffInfo.buffData.buffRemoveStackUpdate) //判断去掉模式
        {
            case BuffRemoveStackUpdateEnum.Clear: //清除效果
                Debug.Log("执行onremove");
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
