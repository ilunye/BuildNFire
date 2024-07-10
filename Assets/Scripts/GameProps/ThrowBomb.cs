using UnityEditor;
using UnityEngine;
using System.Collections;

public class ThrowBomb : MonoBehaviour
{
    public GameObject bomb;  // 炸弹的预制体
    public float InitthrowForce = 1f; // 初始投掷的力量

    public float throwForce;

    public float MaxThrowForce = 50f;

    void Start()
    {
        throwForce = InitthrowForce;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.M)) //如果长按M则累计投掷的力量
        {
            if (throwForce < MaxThrowForce)
            {
                throwForce += Time.deltaTime * 3;
            }
        }
        if (Input.GetKeyUp(KeyCode.M)) // 检测玩家按下投掷按钮（默认是鼠标左键或Ctrl）
        {
            Debug.Log("按下M，扔炸弹");
            Throw();
            throwForce = InitthrowForce;
        }

    }

    /*
    void Throw()
    {
        Vector3 p = new Vector3(0f, 2f, 1f);
        GameObject bomb = Instantiate(Resources.Load("Prefabs/Bomb Red") as GameObject); // 创建炸弹实例
        bomb.transform.localPosition = gameObject.transform.localPosition + p;
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange); // 给炸弹一个向前的力,但不会改变炸弹的位置
    }
*/



    //不使用物理引擎来投掷炸弹
    void Throw()
    {
        bomb = Instantiate(Resources.Load("Prefabs/Bomb Red") as GameObject); // 创建炸弹实例
        Vector3 throwDirection = transform.forward; // 投掷方向为当前对象的前方向
        Vector3 throwVelocity = throwDirection * throwForce; // 计算投掷速度向量
        Vector3 p = new Vector3(0f, 2f, 1f);
        // 设置炸弹的初始位置
        Vector3 startPosition = transform.position + throwDirection + p;

        // 投掷炸弹，以一定的速度沿着投掷方向移动
        StartCoroutine(ThrowBombPosition(startPosition, throwVelocity));
    }

    IEnumerator ThrowBombPosition(Vector3 startPosition, Vector3 throwVelocity)
    {
        float elapsedTime = 0f;
        float throwDuration = 2f; // 投掷的持续时间

        while (elapsedTime < throwDuration && bomb)
        {
            // 根据投掷速度和时间计算新的位置
            Vector3 newPosition = startPosition + throwVelocity * elapsedTime;

            // 更新炸弹的位置
            bomb.transform.position = newPosition;
            Debug.Log("炸弹的当前位置为：" + bomb.transform.localPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}