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
    public GameObject theBombTarget = null;

    private KeyCode keyCodeE;

    public float changeScale = 1f;

    private float startHigh = 0.0001f;
    public int sceneID = 1;
    private float multipleFore = 3f;
    private Vector3 upVector = new Vector3(0f, 0f, 0f);

    public bool target_dist_increasing = true;

    [Command(requiresAuthority = false)]
    public void CmdServerSpawn(string path, Vector3 position, bool isTarget, bool isBomb)
    {
        if(isTarget){
            theBombTarget = Instantiate(Resources.Load(path) as GameObject, position, Quaternion.Euler(90, 0, 0));
            NetworkServer.Spawn(theBombTarget);
        }else if(isBomb){
            bomb = Instantiate(Resources.Load(path) as GameObject, position, Quaternion.identity);
            bomb.GetComponent<NetBomb>().SetTarget(theBombTarget);        // set the bomb target for destroy
            NetworkServer.Spawn(bomb);
            StartCoroutine(ThrowBombPosition(position, transform.forward));
        }
    }
    [ClientRpc]
    public void RpcSetTarget(GameObject target)
    {
        theBombTarget = target;
    }
    [Command(requiresAuthority = false)]
    public void CmdDestroy(GameObject obj)
    {
        NetworkServer.Destroy(obj);
    }

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
        switch (sceneID)
        {
            case 2:
                startHigh = 1.2f;
                MaxThrowForce = 20f;
                multipleFore = 7f;
                upVector = new Vector3(0f, 0.3f, 0f); //make sure the bomb target is above the ground
                break;
        }


    }
    void Update()
    {
        if(!isLocalPlayer) return;
        if (GetComponent<NetCharacter>().Material == NetCharacter.MaterialType.Bomb) //如果玩家捡到炸弹
        {
            // BombImage.SetActive(true);
            if (Input.GetKey(keyCodeE)) //如果长按E则累计投掷的力量
            {
                if (!readytothrow)
                {      // instantiate a target point
                    //Debug.Log("creat bomb target" );
                    readytothrow = true;

                    //Debug.Log(target.transform.position);
                    CmdServerSpawn("Prefabs/Online/bomb_target", transform.position + transform.forward * 1f * throwForce + upVector, true, false);
                }
                else if(theBombTarget){
                    theBombTarget.transform.position = transform.position + transform.forward * (1f * throwForce + 0.6f) + upVector;
                }
                if (target_dist_increasing && throwForce < MaxThrowForce)
                {
                    throwForce += Time.deltaTime * multipleFore;
                    if(throwForce > MaxThrowForce)
                        throwForce = MaxThrowForce;
                    if(throwForce == MaxThrowForce)
                        target_dist_increasing = false;
                }
                else if(!target_dist_increasing && throwForce > InitthrowForce)
                {
                    throwForce -= Time.deltaTime * multipleFore;
                    if(throwForce < InitthrowForce)
                        throwForce = InitthrowForce;
                    if(throwForce == InitthrowForce)
                        target_dist_increasing = true;
                }
            }

            if (Input.GetKeyUp(keyCodeE)) // 检测玩家按下投掷按钮
            {
                readytothrow = false;
                Throw();
                GetComponent<NetCharacter>().Material = NetCharacter.MaterialType.None;

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
        bomb.transform.position = gameObject.transform.position + p;
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange); // 给炸弹一个向前的力,但不会改变炸弹的位置
    }
*/



    //不使用物理引擎来投掷炸弹
    void Throw()
    {
        hasthrow = true;
        Vector3 startPosition = gameObject.transform.position + gameObject.transform.forward * 0.5f + new Vector3(0, 0.5f, 0);
        CmdServerSpawn("Prefabs/Online/Bomb Red", startPosition, false, true);
        // 投掷炸弹，以一定的速度沿着投掷方向移动

    }

    IEnumerator ThrowBombPosition(Vector3 startPosition, Vector3 direction)
    {
        //Debug.Log("direction:" + direction);
        elapsedTime = 0f;

        Vector3 gravity; //增加重力

        elapsedTime += Time.deltaTime;
        //Debug.Log("throwforce1 = " + throwForce);
        while (elapsedTime < throwDuration && bomb)
        {

            // 根据投掷速度和时间计算新的位置
            float x = throwForce * elapsedTime * direction.x;
            float z = throwForce * elapsedTime * direction.z;
            float y = startHigh + 5f * elapsedTime - 10f * 0.5f * Mathf.Pow(elapsedTime, 2);
            gravity = new Vector3(x, y, z);
            //Debug.Log("y" + y);
            Vector3 newPosition = startPosition + gravity;
            // 更新炸弹的位置
            bomb.transform.position = newPosition;
            //Debug.Log("炸弹的当前位置为：" + bomb.transform.position);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        throwForce = InitthrowForce; //在最终都运行结束后回复原值！！
        // BombImage.SetActive(false);
        hasthrow = false;
    }

    public void SetTarget(GameObject target)
    {
        if (target == null)
        {
            CmdDestroy(theBombTarget);
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