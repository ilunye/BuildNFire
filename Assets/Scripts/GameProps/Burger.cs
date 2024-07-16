using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    public BuffData buffData;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay(10f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    // void OnTriggerStay(Collider other){
    //     if(other.tag != "Player")
    //         return;
    //     Debug.Log("Burger");
    //     BuffInfo buffInfo = new BuffInfo();
    //     buffInfo.buffData = buffData;
    //     GetCollisionBuff(buffInfo, gameObject, other.gameObject);
    // }

    // void GetCollisionBuff(BuffInfo buffInfo, GameObject CollObj, GameObject gameobject) //得到碰撞物体的buff信息
    // {
    //     buffInfo.creater = CollObj;
    //     buffInfo.target = gameobject;
    //     buffInfo.durationTime = buffInfo.buffData.DurationTime;
    //     if (buffInfo != null)
    //     {
    //         AddBuff(buffInfo);
    //         Destroy(CollObj);
    //     }
    // }
}

