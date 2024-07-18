using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Mirror;

public class NetBomb : NetworkBehaviour
{
    private float delay = 10f; // 炸弹的延迟时间
    private float explosionForce = 400f; // 爆炸的力量
    private float explosionRadius = 1.5f; // 爆炸的半径
    public GameObject explosionEffect; // 爆炸效果的预制体

    public float countdown; //爆炸倒计时
    public bool hasExploded = false;

    public BuffData buffData; //add buff

    public bool claimed = false; //是否被捡起来
    private float initialVelocityX = 0.2f;//被炸的力
    private float initialVelocityZ = 0.2f;//被炸的力
    private float throwForce = 1f;//被炸的力

    private NetBuffHandler buffHandler;

    private GameObject bombvoiceObj;
    private BombVoice bombVoice;

    private GameObject theBombTarget = null;

    [Command(requiresAuthority = false)]
    public void CmdLittleBombInstantiate(){
        explosionEffect = Instantiate(Resources.Load("Prefabs/Online/littlebomb") as GameObject);
        explosionEffect.transform.localPosition = gameObject.transform.localPosition;
        explosionEffect.transform.localRotation = gameObject.transform.localRotation;
        NetworkServer.Spawn(explosionEffect);
    }
    [Command(requiresAuthority = false)]
    public void CmdDestroy(GameObject obj){
        NetworkServer.Destroy(obj);
    }

    void Start()
    {
        countdown = delay;
        bombvoiceObj = Instantiate(Resources.Load("Audio/bomb_voice") as GameObject);
        bombVoice = bombvoiceObj.GetComponent<BombVoice>();

    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded && !claimed)
        {
            Explode();

        }
        else if (gameObject.transform.position.y < 0 && !hasExploded) //掉出世界爆炸
        {
            Explode();

        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !hasExploded && claimed)
        //如果打到对方玩家 && 炸弹还没爆炸 && 已经被捡起来了
        {
            buffHandler = collision.GetComponent<NetBuffHandler>();
            if (buffHandler.bombbuffinfo != null)
            {
                buffHandler.AddBuff(buffHandler.bombbuffinfo);
            }
            Explode();


        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" || claimed)
            return;
        if (other.GetComponent<NetCharacter>().playerState == NetCharacter.PlayerState.Idle)
        {
            other.GetComponent<NetCharacter>().playerState = NetCharacter.PlayerState.ReadyToClaim;
            if (other.GetComponent<NetCharacter>().Item == null)
            {
                other.GetComponent<NetCharacter>().Item = gameObject;
            }
        }
        if (other.GetComponent<NetCharacter>().playerState == NetCharacter.PlayerState.Claim)
        {
            if (other.GetComponent<NetCharacter>().Item != null)
            {
                if (other.GetComponent<NetCharacter>().Item.name == gameObject.name)
                {
                    claimed = true;
                    other.GetComponent<NetCharacter>().Material = NetCharacter.MaterialType.Bomb;
                    other.GetComponent<NetCharacter>().Item = gameObject;              // set the player's item as itself
                    CmdDestroy(gameObject);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" || claimed)
            return;
        if (other.GetComponent<NetCharacter>().playerState == NetCharacter.PlayerState.ReadyToClaim)
        {
            other.GetComponent<NetCharacter>().playerState = NetCharacter.PlayerState.Idle;
        }
        if (other.GetComponent<NetCharacter>().Item != null && other.GetComponent<NetCharacter>().Item.name == gameObject.name)
        {
            other.GetComponent<NetCharacter>().Item = null;
        }
    }

    public void Explode()
    {
        hasExploded = true;
        // Debug.Log("voiceobj" + bombvoiceObj);
        // Debug.Log("voice" + bombVoice);
        bombVoice.PlayMusic();
        // 显示爆炸效果
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        CmdLittleBombInstantiate();
        // 获取爆炸范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (nearbyObject.tag == "Player")
                {
                    BuffInfo buffInfo = new BuffInfo();
                    buffInfo.buffData = buffData;
                    buffInfo.target = nearbyObject.gameObject;
                    nearbyObject.GetComponent<NetBuffHandler>().AddBuff(buffInfo);
                    nearbyObject.GetComponent<Rigidbody>().freezeRotation = false;
                    nearbyObject.GetComponent<NetCharacter>().freezeTimer = 3f;
                }
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            else
            {
                if (nearbyObject.tag == "Cannon")
                {
                    if (nearbyObject.name.EndsWith("1"))
                    {
                        GameObject g = GameObject.Find("PlayerUI_1");
                        int t = g.GetComponent<WorkFlow>().workFlowPos;
                        t--;
                        if (t < 0)
                            t = 0;
                        g.GetComponent<WorkFlow>().workFlowPos = t;
                        nearbyObject.GetComponent<NetCannon>().prev_state();
                    }
                    else if (nearbyObject.name.EndsWith("2"))
                    {
                        GameObject g = GameObject.Find("PlayerUI_2");
                        int t = g.GetComponent<WorkFlow>().workFlowPos;
                        t--;
                        if (t < 0)
                            t = 0;
                        g.GetComponent<WorkFlow>().workFlowPos = t;
                        nearbyObject.GetComponent<NetCannon>().prev_state();
                    }
                }
            }
        }
        // 销毁炸弹对象
        if(theBombTarget != null){
            CmdDestroy(theBombTarget);
            theBombTarget = null;
        }
        CmdDestroy(gameObject);
        CmdDestroy(explosionEffect);

    }

    public void SetTarget(GameObject target){
        theBombTarget = target;
    }

    void OnDestroy()
    {
        if(theBombTarget != null){
            CmdDestroy(theBombTarget);
            theBombTarget = null;
        }
    }


}