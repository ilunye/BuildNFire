using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//挂在玩家上
public class Character : MonoBehaviour
{
    public bool enabled = true;
    public float PlayerSpeed = 1f; //人物移动速度
    public float sleep = 0f; //冻结时间
    GameObject Player = null; //人物

    private Pack pack; //引用背包
    public GameObject Item = null; // the item that the player holds

    private Transform tr; //创造射线

    public BuffData buffData; //buff

    public Property buffproperty;

    public Animator Anim;
    private AnimatorStateInfo stateInfo;

    public GameObject cam; // the camera
    public bool runSoundFlag = true;
    public VirtualKey virtualKey;

    public enum PlayerState
    {
        Idle,
        Run,
        Punch,
        ReadyToClaim,       // ready to claim objects, that is colliding with sth
        Claim,
        Falling,
        Operating
    }

    public enum MaterialType
    {
        None,
        Wood,
        IronOre,
        Iron,
        GunPowder,
        CannonBall,
        Bomb
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

    public bool isPunch = false;
    public bool isFalling = false;
    public float timer = 0f;
    public float freezeTimer = 3f;
    internal object property;

    public bool enableOut = true;
    private bool IsOut = false;
    public bool InCorner = false;
    // voice
    public GameObject beated_voice; //get beated voice
    public AudioSource beated_voice_source;
    public GameObject get_item;
    public AudioSource get_item_source;

    public GameObject run;
    public AudioSource run_source;
    public GameObject whatudo;
    public AudioSource whatudo_source;

    private GameObject item_fall;
    private AudioSource item_fall_voice;

    private GameObject myCannon;
    private bool network = false;
    private AnimationPlayer animationPlayer;
    private NetworkAnimationPlayer networkAnimationPlayer;
    void OnTriggerStay(Collider other) //get beat
    {
        if (isFalling || (other.tag == "Player" && other.gameObject.GetComponent<Character>().isFalling)) return;
        if (other.tag == "Player" && playerState == PlayerState.Punch && timer < 0.5f)
        {
            timer += Time.deltaTime;
            if (timer > 0.4f)
            {
                if(network){
                    other.GetComponent<Animator>().Play("DAMAGED01");

                    other.GetComponent<NetworkAnimationPlayer>().Play("DAMAGED01");
                }else{
                    other.GetComponent<AnimationPlayer>().Play("DAMAGED01");
                }
                other.gameObject.GetComponent<Character>().isFalling = true;
                other.gameObject.GetComponent<Character>().playerState = PlayerState.Falling;
                beated_voice_source.Play();
                whatudo_source.Play();
            }
        }
    }

    void Awake()
    {
        Anim = GetComponent<Animator>();
        virtualKey = GetComponent<VirtualKey>();
        network = GetComponent<NetworkKey>() != null;
        if(network){
            for(int i=0; i<4; i++){
                myCannon = GameObject.Find("cannon" + i.ToString());
                if(myCannon.GetComponent<Cannon>().claimed == false){
                    myCannon.GetComponent<Cannon>().claimed = true;
                    myCannon.GetComponent<Cannon>().player = gameObject;
                    break;
                }
            } 
            networkAnimationPlayer = GetComponent<NetworkAnimationPlayer>();
        }else{
            animationPlayer = GetComponent<AnimationPlayer>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        pack = GetComponent<Pack>();
        tr = GetComponent<Transform>();
        beated_voice = Instantiate(Resources.Load("Audio/beat") as GameObject);
        beated_voice_source = beated_voice.GetComponent<AudioSource>();
        get_item = Instantiate(Resources.Load("Audio/get_item") as GameObject);
        get_item_source = get_item.GetComponent<AudioSource>();
        run = Instantiate(Resources.Load("Audio/running") as GameObject);
        run_source = run.GetComponent<AudioSource>();
        whatudo = Instantiate(Resources.Load("Audio/whatareudoing") as GameObject);
        whatudo_source = whatudo.GetComponent<AudioSource>();
        item_fall = Instantiate(Resources.Load("Audio/itemfall") as GameObject);
        item_fall_voice = item_fall.GetComponent<AudioSource>();

        if(cam == null){
            cam = GameObject.Find("Fake_Camera");
        }
        Debug.Assert(cam != null, "cam is null");
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
        if (!enabled) return;
        if (!enableOut)
        {
            if (transform.position.x < x_bound_left || transform.position.x > x_bound_right || transform.position.z < z_bound_down || transform.position.z > z_bound_up)
            {
                IsOut = true;
            }
            else
                IsOut = false;
        }
        else
        {
            IsOut = false;
        }

        if (playerState == PlayerState.Falling && Material != MaterialType.None)
        {        // holding something
            if (Material == MaterialType.Wood)
            {
                GameObject g = Instantiate(Resources.Load("Prefabs/Wood") as GameObject);
                g.transform.position = transform.position + new Vector3(0, 0.5f, 0);
                g.GetComponent<CollectableMaterials>().WillDisappear = false;
            }
            else if (Material == MaterialType.IronOre)
            {
                GameObject g = Instantiate(Resources.Load("Prefabs/Rock_03") as GameObject);
                g.transform.position = transform.position + new Vector3(0, 0.5f, 0);
                g.GetComponent<CollectableMaterials>().WillDisappear = false;
            }
            else if (Material == MaterialType.Iron)
            {
                GameObject g = Instantiate(Resources.Load("Prefabs/ConcreteTubes") as GameObject);
                g.transform.position = transform.position + new Vector3(0, 0.5f, 0);
                g.GetComponent<CollectableMaterials>().WillDisappear = false;
            }
            else if (Material == MaterialType.GunPowder)
            {
                GameObject g = Instantiate(Resources.Load("Prefabs/explosiveBarrel") as GameObject);
                g.transform.position = transform.position + new Vector3(0, 0.5f, 0);
                g.GetComponent<CollectableMaterials>().WillDisappear = false;
            }
            else if (Material == MaterialType.CannonBall)
            {
                GameObject g = Instantiate(Resources.Load("Prefabs/projectile") as GameObject);
                g.transform.position = transform.position + new Vector3(0, 0.5f, 0);
                g.GetComponent<CollectableMaterials>().WillDisappear = false;
            }
            else if(Material == MaterialType.Bomb){
                GetComponent<ThrowBomb>().SetTarget(null);
                GetComponent<ThrowBomb>().ResetThrowForce();
            }
            Material = MaterialType.None;
        }
        stateInfo = Anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle") && playerState != PlayerState.ReadyToClaim)
        {
            playerState = PlayerState.Idle;
        }
        if (playerState == PlayerState.Idle)
        {
            isPunch = false;
            isFalling = false;
            timer = 0f;
        }
        Motion();
        Motion_Voice();

        if (sleep != 0)
        {
            //Debug.Log("玩家休眠");
            PlayerSpeed = 0; //玩家休眠
            if(network){
                Anim.Play("StunnedLoop");
                GetComponent<NetworkAnimationPlayer>().Play("StunnedLoop");
            }else{
                GetComponent<AnimationPlayer>().Play("StunnedLoop");
            }
            gameObject.GetComponent<Character>().isFalling = true;
            gameObject.GetComponent<Character>().playerState = PlayerState.Falling;

        }
        // if(Anim.name != "PunchRight"){
        //     Motion();
        //     pack.ShowPack(); //按下K展示背包
        //     RayCaseObj();  //拾捡物品
        // }
        if(playerState == PlayerState.Falling){
            // 更改rotation, y轴旋转不变
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }

        if(freezeTimer > 0f){
            freezeTimer -= Time.deltaTime;
            if(freezeTimer <= 0f){
                GetComponent<Rigidbody>().freezeRotation = true;
            }
        }

    }

    private void Motion_Voice()
    {
        if ((virtualKey.getKey[0] || (virtualKey.getKey[1]) || (virtualKey.getKey[2]) || (virtualKey.getKey[3])) && runSoundFlag)
        {
            run_source.Play();
            runSoundFlag = false;
        }else if (!(virtualKey.getKey[0]) && !(virtualKey.getKey[1]) && !(virtualKey.getKey[2]) && !(virtualKey.getKey[3]))
        {
            run_source.Stop();
            runSoundFlag = true;
        }
        if(playerState == PlayerState.Falling || playerState == PlayerState.Operating){
            run_source.Stop();
            runSoundFlag = true;
        }
    }


    private void Motion()
    {
        // go up
        if (playerState != PlayerState.Punch && playerState != PlayerState.Claim && playerState != PlayerState.Falling && playerState != PlayerState.Operating)
        {

            if ((virtualKey.getKey[0]))
            {
                //Debug.Log("向前走");
                // 向世界坐标系得z轴方向移动

                Vector3 p = transform.localPosition;
                if ((!InCorner) && (!IsOut || (IsOut && (transform.position.z < z_bound_down || ((transform.position.x > x_bound_right || transform.position.x < x_bound_left) && transform.position.z < z_bound_up)))))
                {
                    if (PlayerSpeed > 0)
                        p += cam.transform.forward * PlayerSpeed * Time.deltaTime;
                    else if (transform.position.z > z_bound_down)
                        p += cam.transform.forward * PlayerSpeed * Time.deltaTime;
                }
                transform.localPosition = p;
                Idle2Run();
                Rotate(Direction.Forward);
            }
            if ((virtualKey.getKeyUp[0]))
                Run2Idel();

            // go down
            if ((virtualKey.getKey[1]))
            {

                Vector3 p = transform.localPosition;
                if (InCorner || !IsOut || (IsOut && (transform.position.z > z_bound_up || ((transform.position.x > x_bound_right || transform.position.x < x_bound_left) && transform.position.z > z_bound_down))))
                {
                    if (PlayerSpeed > 0)
                        p -= cam.transform.forward * PlayerSpeed * Time.deltaTime;
                    else if (transform.position.z < z_bound_up)
                        p -= cam.transform.forward * PlayerSpeed * Time.deltaTime;
                }

                transform.localPosition = p;
                Idle2Run();
                Rotate(Direction.Backward);
            }
            if ((virtualKey.getKeyUp[1]))
                Run2Idel();

            // go left
            if ((virtualKey.getKey[2]))
            {

                Vector3 p = transform.localPosition;
                if (InCorner || !IsOut || (IsOut && (transform.position.x > x_bound_right || ((transform.position.z > z_bound_up || transform.position.z < z_bound_down) && transform.position.x > x_bound_left))))
                {
                    if (PlayerSpeed > 0)
                        p -= cam.transform.right * PlayerSpeed * Time.deltaTime;
                    else if (transform.position.x < x_bound_right)
                        p -= cam.transform.right * PlayerSpeed * Time.deltaTime;
                }
                transform.localPosition = p;
                Idle2Run();
                Rotate(Direction.Left);
            }
            if ((virtualKey.getKeyUp[2]))
                Run2Idel();

            // go right
            if ((virtualKey.getKey[3]))
            {

                Vector3 p = transform.localPosition;
                if ((!InCorner) && (!IsOut || (IsOut && (transform.position.x < x_bound_left || ((transform.position.z > z_bound_up || transform.position.z < z_bound_down) && transform.position.x < x_bound_right)))))
                {
                    if (PlayerSpeed > 0)
                        p += cam.transform.right * PlayerSpeed * Time.deltaTime;
                    else if (transform.position.x > x_bound_left)
                        p += cam.transform.right * PlayerSpeed * Time.deltaTime;
                }
                transform.localPosition = p;
                Idle2Run();
                Rotate(Direction.Right);
            }
            if ((virtualKey.getKeyUp[3]))
                Run2Idel();



        }
        if ((virtualKey.getKeyUp[4])) //改成getkeyup，长按E后再播放投掷动画
        {      // E
            if (stateInfo.IsName("CastingLoop") || stateInfo.IsName("CastingLoop 2"))
            {
                playerState = PlayerState.Operating;
            }
            else if (playerState == PlayerState.Idle && Material == MaterialType.None)
            {   // no items in hand
                if(network){
                    GetComponent<NetworkAnimationPlayer>().Play("PunchRight");
                    Anim.Play("PunchRight");
                }else{
                    GetComponent<AnimationPlayer>().Play("PunchRight");
                }
                isPunch = false;
                playerState = PlayerState.Punch;
            }
            else if ((playerState == PlayerState.Idle || playerState == PlayerState.ReadyToClaim) && Material != MaterialType.None && Material != MaterialType.Bomb)
            //item in hand ,press E put down
            {
                GameObject obj = null;
                switch (Material)
                {
                    case MaterialType.Wood:
                        obj = Instantiate(Resources.Load("Prefabs/Wood") as GameObject, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        break;
                    case MaterialType.IronOre:
                        obj = Instantiate(Resources.Load("Prefabs/Rock_03") as GameObject, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        break;
                    case MaterialType.Iron:
                        obj = Instantiate(Resources.Load("Prefabs/ConcreteTubes") as GameObject, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        break;
                    case MaterialType.GunPowder:
                        obj = Instantiate(Resources.Load("Prefabs/explosiveBarrel") as GameObject, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        break;
                    case MaterialType.CannonBall:
                        obj = Instantiate(Resources.Load("Prefabs/projectile") as GameObject, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        break;
                }
                obj.GetComponent<CollectableMaterials>().WillDisappear = false;
                RaycastHit hit;
                if(Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), transform.forward, out hit, 1f)){
                    obj.transform.position = new Vector3(obj.transform.position.x, 0.5f, obj.transform.position.z) - transform.forward * 0.3f;
                }
                else{
                    obj.transform.position = new Vector3(obj.transform.position.x, 0.5f, obj.transform.position.z) + transform.forward * 0.3f;
                }
                item_fall_voice.Play();
                Material = MaterialType.None;
                Item = null;
            }
            else if (playerState == PlayerState.ReadyToClaim && Material == MaterialType.None)
            // no item in hand and ready to grab item
            {
                playerState = PlayerState.Claim;
                if(network){
                    Anim.Play("Gathering");
                    GetComponent<NetworkAnimationPlayer>().Play("Gathering");
                }else{
                    GetComponent<AnimationPlayer>().Play("Gathering");
                }
                get_item_source.Play();
            }
        }
    }

    private void Idle2Run()
    {
        playerState = PlayerState.Run;
        if(network){
            Anim.Play("Run_norm");
            GetComponent<NetworkAnimationPlayer>().Play("Run_norm");
        }else{
            GetComponent<AnimationPlayer>().Play("Run_norm");
        }
    }

    private void Run2Idel()
    {
        playerState = PlayerState.Idle;
        if(network){
            Anim.Play("Idle");
            GetComponent<NetworkAnimationPlayer>().Play("Idle");
        }else{
            GetComponent<AnimationPlayer>().Play("Idle");
        }
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
}
