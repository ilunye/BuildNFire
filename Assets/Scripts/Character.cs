using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//挂在玩家上
public class Character : MonoBehaviour
{
    public float PlayerSpeed = 1f; //人物移动速度
    GameObject Player = null; //人物

    public Pack pack; //引用背包

    private Transform tr; //创造射线

    private Animator Anim;

    public GameObject cam; // the camera
    public enum PlayerState
    {
        Idle,
        Run,
        Punch,
        Attack,
        Dead
    }

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

    void Awake(){
        Anim = GetComponent<Animator>();
        Anim.SetBool("Running", false);
        Anim.SetInteger("Trans_State", 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        pack = GetComponent<Pack>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // PlayerMove(); //人物移动
        if(playerState == PlayerState.Idle){
            transState = TransState.init;
            isPunch = false;
        }
        Motion();
        pack.ShowPack(); //按下K展示背包
        RayCaseObj();  //拾捡物品
        // if(Anim.name != "PunchRight"){
        //     Motion();
        //     pack.ShowPack(); //按下K展示背包
        //     RayCaseObj();  //拾捡物品
        // }

    }

    private void Motion(){
        // go up
        if(Input.GetKey(KeyCode.W)){
            // 向世界坐标系得z轴方向移动
            Vector3 p = transform.localPosition;
            p += cam.transform.forward * PlayerSpeed * Time.deltaTime;
            transform.localPosition = p;
            Idle2Run();
            Rotate(Direction.Forward);
        }
        if(Input.GetKeyUp(KeyCode.W))
            Run2Idel();

        // go down
        if(Input.GetKey(KeyCode.S)){
            Vector3 p = transform.localPosition;
            p -= cam.transform.forward * PlayerSpeed * Time.deltaTime;
            transform.localPosition = p;
            Idle2Run();
            Rotate(Direction.Backward);
        }
        if(Input.GetKeyUp(KeyCode.S))
            Run2Idel();

        // go left
        if(Input.GetKey(KeyCode.A)){
            Vector3 p = transform.localPosition;
            p -= cam.transform.right * PlayerSpeed * Time.deltaTime;
            transform.localPosition = p;
            Idle2Run();
            Rotate(Direction.Left);
        }
        if(Input.GetKeyUp(KeyCode.A))
            Run2Idel();

        // go right
        if(Input.GetKey(KeyCode.D)){
            Vector3 p = transform.localPosition;
            p += cam.transform.right * PlayerSpeed * Time.deltaTime;
            transform.localPosition = p;
            Idle2Run();
            Rotate(Direction.Right);
        }
        if(Input.GetKeyUp(KeyCode.D))
            Run2Idel();
        
        if(Input.GetKeyDown(KeyCode.E)){
            Anim.Play("PunchRight");
            isPunch = false;
            playerState = PlayerState.Punch;
        }
        // if(Input.GetKeyUp(KeyCode.E)){
        //     Anim.Play("Idle");
        //     isPunch = false;
        //     playerState = PlayerState.Idle;
        // }
    }

    private void Idle2Run(){
        playerState = PlayerState.Run;
        Anim.SetBool("Running", true);
        Anim.Play("Run_norm");
    }

    private void Run2Idel(){
        playerState = PlayerState.Idle;
        Anim.SetBool("Running", false);
        Anim.Play("Idle");
    }

    private void Rotate(Direction dir){
        // rotate smoothly
        // 半秒转180度
        switch(dir){
            case Direction.Forward:
                transform.forward = Vector3.LerpUnclamped(transform.forward, cam.transform.forward, 0.5f);
                break;
            case Direction.Backward:
                // transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 180, 0);
                if(transform.forward == new Vector3(0, 0, 1))
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
        if(Physics.Raycast(tr.position, tr.forward, out hit, 2.0f))
        //将射线碰撞信息存储在 hit 变量中
        {
            Debug.Log ("射线击中:" + hit.collider.gameObject.name + "\n tag:" + hit.collider.tag);
            GameObject gameObj = hit.collider.gameObject; //获取碰到的物品
            ObjectItem obj =(ObjectItem)gameObj.GetComponent<ObjectItem>();
            if(obj != null)
            {
                Debug.Log("捡到的物品" + obj.name);
                obj.IsCheck = true;
                if(Input.GetKeyDown(KeyCode.E)) //按下E捡东西
                {
                    Debug.Log("按下E");
                    pack.GetItem(obj);
                    if(obj.count == 0) //当前物品拾捡完成
                    {
                        Destroy(gameObj);
                    }
                }
            }
        }
    }
   
}
