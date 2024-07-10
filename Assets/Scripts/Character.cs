using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//挂在玩家上
public class Character : MonoBehaviour
{
    public float PlayerSpeed = 1f; //人物移动速度
    public float sleep = 0f; //冻结时间
    GameObject Player = null; //人物


    public Pack pack; //引用背包

    private Transform tr; //创造射线

    public BuffData buffData; //buff

    public Property buffproperty;

    private Animator Anim;
    private AnimatorStateInfo stateInfo;

    public GameObject cam; // the camera
    public bool wasd = true;
    private KeyCode[] keycodes;

    public enum PlayerState
    {
        Idle,
        Run,
        Punch,
        ReadyToClaim,       // ready to claim objects, that is colliding with sth
        Claim,
        Falling,
        Dead
    }

    public enum MaterialType
    {
        None,
        Wood,
        Stone
    }

    public MaterialType Material = MaterialType.None;

    private enum Direction
    {
        Forward,
        Backward,
        Left,
        Right
    }

    private enum TransState
    {
        init,
        IdletoPunchRight,
        IdletoRun
    }

    public PlayerState playerState = PlayerState.Idle;
    private TransState transState = TransState.init;

    public bool isPunch = false;
    public bool isFalling = false;
    public float timer = 0f;
    internal object property;

    private bool IsOut = false;
    public bool InCorner = false;

    void OnTriggerStay(Collider other){
        if(isFalling || (other.tag == "Player" && other.gameObject.GetComponent<Character>().isFalling)) return;
        if(other.tag == "Player" && playerState == PlayerState.Punch && timer < 0.5f){
            timer += Time.deltaTime;
            if (timer > 0.4f)
            {
                other.gameObject.GetComponent<Animator>().Play("DAMAGED01");
                other.gameObject.GetComponent<Character>().isFalling = true;
                other.gameObject.GetComponent<Character>().playerState = PlayerState.Falling;
            }
        }
    }

    void Awake()
    {
        Anim = GetComponent<Animator>();
        Anim.SetBool("Running", false);
        Anim.SetInteger("Trans_State", 0);
        if (wasd == true)
        {
            keycodes = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.E };
        }
        else
        {
            keycodes = new KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Return };
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        pack = GetComponent<Pack>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    /*
    (794.25, 977.17) left down
(794.25, 987.02) left up
(804.89, 977.17) right down

    */
    private float x_bound_left = 794.25f;
    private float x_bound_right = 804.89f;
    private float z_bound_down = 977.17f;
    private float z_bound_up = 987.02f;
    void Update()
    {
        if(transform.position.x < x_bound_left || transform.position.x > x_bound_right || transform.position.z < z_bound_down || transform.position.z > z_bound_up){
            Debug.Log("Out of the map");   
            IsOut = true;
        }
        else
            IsOut = false;
        stateInfo = Anim.GetCurrentAnimatorStateInfo(0);
        // PlayerMove(); //人物移动
        if (stateInfo.IsName("Idle") && playerState != PlayerState.ReadyToClaim)
        {
            playerState = PlayerState.Idle;
        }
        if (playerState == PlayerState.Idle)
        {
            transState = TransState.init;
            isPunch = false;
            isFalling = false;
            timer = 0f;
        }
        Motion();
        pack.ShowPack(); //按下K展示背包
        RayCaseObj();  //拾捡物品

        if (sleep != 0)
        {
            Sleep();
        }
        // if(Anim.name != "PunchRight"){
        //     Motion();
        //     pack.ShowPack(); //按下K展示背包
        //     RayCaseObj();  //拾捡物品
        // }

    }

    private void Sleep()
    {
        PlayerSpeed = 0;
    }


    private void Motion()
    {
        // go up
        if (playerState != PlayerState.Punch && playerState != PlayerState.Claim && playerState != PlayerState.Falling)
        {
            if (Input.GetKey(keycodes[0]))
            {
                // 向世界坐标系得z轴方向移动
                Vector3 p = transform.localPosition;
                if((!InCorner) && (!IsOut || (IsOut && (transform.position.z < z_bound_down || ((transform.position.x > x_bound_right || transform.position.x < x_bound_left) && transform.position.z < z_bound_up))))){
                    Debug.Log("Move up");
                    p += cam.transform.forward * PlayerSpeed * Time.deltaTime;
                }
                transform.localPosition = p;
                Idle2Run();
                Rotate(Direction.Forward);
            }
            if (Input.GetKeyUp(keycodes[0]))
                Run2Idel();

            // go down
            if (Input.GetKey(keycodes[1]))
            {
                Vector3 p = transform.localPosition;
                if(InCorner || !IsOut || (IsOut && (transform.position.z > z_bound_up || ((transform.position.x > x_bound_right || transform.position.x < x_bound_left) && transform.position.z > z_bound_down))))
                    p -= cam.transform.forward * PlayerSpeed * Time.deltaTime;
                transform.localPosition = p;
                Idle2Run();
                Rotate(Direction.Backward);
            }
            if (Input.GetKeyUp(keycodes[1]))
                Run2Idel();

            // go left
            if (Input.GetKey(keycodes[2]))
            {
                Vector3 p = transform.localPosition;
                if(InCorner || !IsOut || (IsOut && (transform.position.x > x_bound_right || ((transform.position.z > z_bound_up || transform.position.z < z_bound_down) && transform.position.x > x_bound_left))))
                    p -= cam.transform.right * PlayerSpeed * Time.deltaTime;
                transform.localPosition = p;
                Idle2Run();
                Rotate(Direction.Left);
            }
            if (Input.GetKeyUp(keycodes[2]))
                Run2Idel();

            // go right
            if (Input.GetKey(keycodes[3]))
            {
                Vector3 p = transform.localPosition;
                if((!InCorner) && (!IsOut || (IsOut && (transform.position.x < x_bound_left || ((transform.position.z > z_bound_up || transform.position.z < z_bound_down) && transform.position.x < x_bound_right)))))
                    p += cam.transform.right * PlayerSpeed * Time.deltaTime;
                transform.localPosition = p;
                Idle2Run();
                Rotate(Direction.Right);
            }
            if (Input.GetKeyUp(keycodes[3]))
                Run2Idel();

        }
        if (Input.GetKeyDown(keycodes[4]))
        {      // E
            if (playerState == PlayerState.Idle && Material == MaterialType.None)
            {   // no items in hand
                Anim.Play("PunchRight");
                isPunch = false;
                playerState = PlayerState.Punch;
            }
            else if (playerState == PlayerState.Idle && Material != MaterialType.None)
            {
                Material = MaterialType.None;
            }
            else if (playerState == PlayerState.ReadyToClaim && Material == MaterialType.None)
            {
                playerState = PlayerState.Claim;
                Anim.Play("Gathering");
            }
        }
    }

    private void Idle2Run()
    {
        playerState = PlayerState.Run;
        Anim.SetBool("Running", true);
        Anim.Play("Run_norm");
    }

    private void Run2Idel()
    {
        playerState = PlayerState.Idle;
        Anim.SetBool("Running", false);
        Anim.Play("Idle");
    }

    private void Rotate(Direction dir)
    {
        // rotate smoothly
        // 半秒转180度
        switch (dir)
        {
            case Direction.Forward:
                transform.forward = Vector3.LerpUnclamped(transform.forward, cam.transform.forward, 0.5f);
                break;
            case Direction.Backward:
                // transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 180, 0);
                if (transform.forward == new Vector3(0, 0, 1))
                    transform.forward = new Vector3(0.01f, 0, 1f);
                transform.forward = Vector3.LerpUnclamped(transform.forward, -cam.transform.forward, 0.5f);
                break;
            case Direction.Left:
                // transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 90, 0);
                transform.forward = Vector3.LerpUnclamped(transform.forward, -cam.transform.right, 0.5f);
                break;
            case Direction.Right:
                // transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 90, 0);
                transform.forward = Vector3.LerpUnclamped(transform.forward, cam.transform.right, 0.5f);
                break;
        }
    }


    private void RayCaseObj()
    {
        //创建射线
        Debug.DrawRay(tr.position, tr.forward * 2.0f, Color.green);
        RaycastHit hit;
        //如果碰撞
        if (Physics.Raycast(tr.position, tr.forward, out hit, 2.0f))
        //将射线碰撞信息存储在 hit 变量中
        {
            Debug.Log("射线击中:" + hit.collider.gameObject.name + "\n tag:" + hit.collider.tag);
            GameObject gameObj = hit.collider.gameObject; //获取碰到的物品
            ObjectItem obj = (ObjectItem)gameObj.GetComponent<ObjectItem>();
            if (obj != null)
            {
                Debug.Log("捡到的物品" + obj.name);
                obj.IsCheck = true;
                if (Input.GetKeyDown(keycodes[4])) //按下E捡东西
                {
                    Debug.Log("按下E");
                    pack.GetItem(obj);
                    if (obj.IsGrab == true) //当前物品拾捡完成
                    {
                        Destroy(gameObj);
                    }
                }
            }
        }
    }

}
