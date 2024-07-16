using UnityEditor;
using UnityEngine;
using System.Collections;
using Mirror;

public class NetThrowBomb : NetworkBehaviour
{
    public GameObject bomb;  // 炸弹的预制体
    private float InitthrowForce = 1f; // 初始投掷的力量

    private float throwDuration = 5f; // 投掷的持续时间
    private float elapsedTime = 0f; //计算投掷的时间

    public float throwForce; //投掷的力量

    private float MaxThrowForce = 5f; //投掷允許的最大力量
    private float IntervelTime = 0f; //炮彈發射的間隔時間

    private bool wasd;

    public bool hasthrow = false;
    // public GameObject BombImage;     //存放炸弹的显示图像

    private bool readytothrow = false;
    private GameObject theBombTarget = null;

    private KeyCode keyCodeE;

    void Start()
    {
        throwForce = InitthrowForce;
        wasd = GetComponent<NetCharacter>().wasd; //分开两个角色的控制键
        if (wasd)
        {
            keyCodeE = KeyCode.E;
        }
        else
        {
            keyCodeE = KeyCode.Return;
        }
       
    }
    void Update()
    {
        if (GetComponent<NetCharacter>().Material == NetCharacter.MaterialType.Bomb) //如果玩家捡到炸弹
        {
            // BombImage.SetActive(true);
            if (Input.GetKey(keyCodeE)) //如果长按E则累计投掷的力量
            {
                if(!readytothrow){      // instantiate a target point
                    readytothrow = true;
                    GameObject target = Instantiate(Resources.Load("Prefabs/bomb_target") as GameObject);
                    target.transform.position = transform.position + transform.forward * 1f * throwForce;
                    theBombTarget = target;
                }else
                    theBombTarget.transform.position = transform.position + transform.forward * (1f * throwForce + 0.6f);
                if (throwForce < MaxThrowForce)
                {
                    throwForce += Time.deltaTime * 3f;
                }
            }

            if (Input.GetKeyUp(keyCodeE)) // 检测玩家按下投掷按钮
            {
                readytothrow = false;
                Throw();
                GetComponent<NetCharacter>().Material =NetCharacter.MaterialType.None;

            }
        }
        // else
            // BombImage.SetActive(false);


    }

    /*
    void Throw() //使用rigidbody和addforce函数，问题是只能显示动画无法改变炸弹的位置因此无法检测到碰撞
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
        hasthrow = true;
        bomb = Instantiate(Resources.Load("Prefabs/Bomb Red") as GameObject); // 创建炸弹实例
        // 设置炸弹的初始位置
        bomb.transform.localPosition = gameObject.transform.localPosition + gameObject.transform.forward * 0.5f;
        Vector3 startPosition = bomb.transform.localPosition;

        bomb.GetComponent<Bomb>().SetTarget(theBombTarget);        // set the bomb target for destroy

        // 投掷炸弹，以一定的速度沿着投掷方向移动
        StartCoroutine(ThrowBombPosition(startPosition, transform.forward));

    }

    IEnumerator ThrowBombPosition(Vector3 startPosition, Vector3 direction)
    {
        //Debug.Log("開始位置" + startPosition);
        elapsedTime = 0f;

        Vector3 gravity; //增加重力

        elapsedTime += Time.deltaTime;
        //Debug.Log("throwforce1 = " + throwForce);
        while (elapsedTime < throwDuration && bomb)
        {
            //Debug.Log("循环" + elapsedTime);
            // 根据投掷速度和时间计算新的位置
            float x = throwForce * elapsedTime * direction.x;
            float z = throwForce * elapsedTime * direction.z;
            float y = 0.0001f + 5f * elapsedTime - 10.0f * 0.5f * Mathf.Pow(elapsedTime, 2);
            gravity = new Vector3(x, y, z);
            Vector3 newPosition = startPosition + gravity;

            // 更新炸弹的位置
            bomb.transform.position = newPosition;
            //Debug.Log("炸弹的当前位置为：" + bomb.transform.localPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        throwForce = InitthrowForce; //在最终都运行结束后回复原值！！
        // BombImage.SetActive(false);
        hasthrow = false;
    }

    public void SetTarget(GameObject target)
    {
        if(target == null){
            Destroy(theBombTarget);
            readytothrow = false;
        }
        theBombTarget = target;
    }

    public void SetReadyToThrow(bool ready)
    {
        readytothrow = ready;
    }

    public void ResetThrowForce()
    {
        throwForce = InitthrowForce;
    }
}