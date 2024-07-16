using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{

    public LinkedList<BuffInfo> buffList = new LinkedList<BuffInfo>(); //存放buff的链表
    private LinkedList<BuffInfo> debuffList = new LinkedList<BuffInfo>(); //debuff link
    public BuffInfo CurrBuffInfo; //现在生效的buff

    public Bomb bomb;
    public GameObject CollObj;
    public ThrowBomb throwBomb;

    public BuffInfo bombbuffinfo;
    public GameObject eat_burger;
    public AudioSource eat_burger_source;

    public GameObject dejavu;
    public AudioSource dejavu_source;

    private GameObject get_lock;
    private AudioSource get_lock_voice;
    private GameObject time_reverse;
    private AudioSource time_reverse_voice;
    private Magnet magnet;
    private GameObject magnetObj;
    // Start is called before the first frame update
    void Start()
    {
        throwBomb = gameObject.GetComponent<ThrowBomb>();
        eat_burger_source = GameObject.Find("Audio/eatburger").GetComponent<AudioSource>();
        dejavu_source = GameObject.Find("Audio/dejavu").GetComponent<AudioSource>();
        get_lock_voice = GameObject.Find("Audio/lock").GetComponent<AudioSource>();
        time_reverse_voice = GameObject.Find("Audio/hourglass").GetComponent<AudioSource>();

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
                    GetCollisionBuff(buffInfo, CollObj);
                    break;
                case "Clock":
                    buffInfo.buffData = CollObj.GetComponent<Clock>().buffData;
                    time_reverse_voice.Play();
                    GetCollisionBuff(buffInfo, CollObj);
                    break;
                case "Lock":
                    buffInfo.buffData = CollObj.GetComponent<Lock>().buffData;
                    get_lock_voice.Play();
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
                case "Magnet":
                    magnet = CollObj.GetComponent<Magnet>();
                    magnet.DealWithMagnet(gameObject);
                    magnetObj = CollObj;
                    Renderer renderer = magnet.GetComponent<Renderer>();
                    renderer.enabled = false;
                    Invoke("DestoryMagnet", 3f);

                    break;
                case "Box":
                    Destroy(CollObj);
                    Deal_With_Box();

                    break;

                default:
                    break;

            }

        }
    }

    private void Deal_With_Box()
    {
        int randomInt = UnityEngine.Random.Range(1, 8);
        GameObject randomObj;
        switch (randomInt)
        {
            case 1:
                randomObj = Instantiate(Resources.Load("Prefabs/burger_1_lod0") as GameObject);
                break;
            case 2:
                randomObj = Instantiate(Resources.Load("Prefabs/Lock Silver") as GameObject);
                break;
            case 3:
                randomObj = Instantiate(Resources.Load("Prefabs/Hourglass Green 1") as GameObject);
                break;
            case 4:
                randomObj = Instantiate(Resources.Load("Prefabs/Magnet") as GameObject);
                break;
            case 5:
                randomObj = Instantiate(Resources.Load("Prefabs/Bomb Red") as GameObject);
                break;
            case 6:
                randomObj = Instantiate(Resources.Load("Prefabs/Rock_03") as GameObject);
                break;
            case 7:
                randomObj = Instantiate(Resources.Load("Prefabs/ConcreteTubes") as GameObject);
                break;
            default:
                randomObj = Instantiate(Resources.Load("Prefabs/Wood") as GameObject);
                break;

        }
        if (randomObj != null)
        {
            randomObj.transform.position = gameObject.transform.position;
        }

    }

    private void DestoryMagnet()
    {

        Destroy(magnetObj);

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

        if (buffInfo.buffData.BuffID == 1)
        {
            foreach (var TempbuffInfo in buffList)
            {
                TempbuffInfo.durationTime = 0;
            }
            buffList.AddLast(buffInfo); //添加到buffList的末尾
        }
        else
        {
            foreach (var TempbuffInfo1 in debuffList)
            {
                TempbuffInfo1.durationTime = 0;
            }
            debuffList.AddLast(buffInfo); //添加到debuffList的末尾
        }

        buffInfo.durationTime = buffInfo.buffData.DurationTime;
        buffInfo.buffData.OnCreate.Apply(buffInfo); //启动buff

        //根据priority对buffList进行排序
        SortBuffList(buffList);
    }

    public void RemoveBuff(BuffInfo buffInfo) //去掉buff
    {
        switch (buffInfo.buffData.buffRemoveStackUpdate) //判断去掉模式
        {
            case BuffRemoveStackUpdateEnum.Clear: //清除效果
                buffInfo.buffData.OnRemove.Apply(buffInfo);
                buffList.Remove(buffInfo);
                debuffList.Remove(buffInfo);
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
        foreach (var buffInfo in debuffList) //遍历debuffList
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
