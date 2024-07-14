using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bomb : MonoBehaviour
{
    private float delay = 10f; // 炸弹的延迟时间
    private float explosionForce = 700f; // 爆炸的力量
    private float explosionRadius = 1.5f; // 爆炸的半径
    public GameObject explosionEffect; // 爆炸效果的预制体

    public float countdown; //爆炸倒计时
    public bool hasExploded = false;

    public BuffData buffData; //add buff

    public bool claimed = false; //是否被捡起来
    private float initialVelocityX=0.2f;//被炸的力
    private float initialVelocityZ=0.2f;//被炸的力
    private float throwForce=1f;//被炸的力

    private BuffHandler buffHandler;

    private GameObject bombvoiceObj;
    private BombVoice bombVoice;

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
        else if (gameObject.transform.localPosition.y < 0 && !hasExploded) //掉出世界爆炸
        {
            Explode();

        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !hasExploded && claimed)
        //如果打到对方玩家 && 炸弹还没爆炸 && 已经被捡起来了
        {
            buffHandler = collision.GetComponent<BuffHandler>();
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
        if (other.GetComponent<Character>().playerState == Character.PlayerState.Idle){
            other.GetComponent<Character>().playerState = Character.PlayerState.ReadyToClaim;
            if(other.GetComponent<Character>().Item == null){
                other.GetComponent<Character>().Item = gameObject;
            }
        }
        if (other.GetComponent<Character>().playerState == Character.PlayerState.Claim)
        {
            if(other.GetComponent<Character>().Item != null){
                if(other.GetComponent<Character>().Item.name == gameObject.name){
                    claimed = true;
                    other.GetComponent<Character>().Material = Character.MaterialType.Bomb;
                    other.GetComponent<Character>().Item = gameObject;              // set the player's item as itself
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag != "Player" || claimed)
            return;
        if(other.GetComponent<Character>().playerState == Character.PlayerState.ReadyToClaim){
            other.GetComponent<Character>().playerState = Character.PlayerState.Idle;
        }
        if(other.GetComponent<Character>().Item != null && other.GetComponent<Character>().Item.name == gameObject.name){
            other.GetComponent<Character>().Item = null;
        }
    }

    public void Explode(Collider other)    //被炸飞or不被炸飞
    {
        
        if(other.CompareTag("Player")){
            Rigidbody r = other.gameObject.GetComponent<Rigidbody>();
            r.AddForce(Vector3.up*throwForce+new Vector3(initialVelocityX,0,initialVelocityZ ),ForceMode.Impulse);
        }
        
        hasExploded = true;
        // Debug.Log("voiceobj" + bombvoiceObj);
        // Debug.Log("voice" + bombVoice);
        // bombVoice.PlayMusic();
        // 显示爆炸效果
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        explosionEffect = Instantiate(Resources.Load("Prefabs/Particle System") as GameObject);
        explosionEffect.transform.localPosition = gameObject.transform.localPosition;
        explosionEffect.transform.localRotation = gameObject.transform.localRotation;
        // 获取爆炸范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        // 销毁炸弹对象
        Destroy(gameObject);
    }
    public void Explode(){
        hasExploded = true;
        // Debug.Log("voiceobj" + bombvoiceObj);
        // Debug.Log("voice" + bombVoice);
        bombVoice.PlayMusic();
        // 显示爆炸效果
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        explosionEffect = Instantiate(Resources.Load("Prefabs/Particle System") as GameObject);
        explosionEffect.transform.localPosition = gameObject.transform.localPosition;
        explosionEffect.transform.localRotation = gameObject.transform.localRotation;
        // 获取爆炸范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if(nearbyObject.tag == "Player"){
                    // Debug.Log("bomb hit player");
                    BuffInfo buffInfo = new BuffInfo();
                    buffInfo.buffData = buffData;
                    buffInfo.target = nearbyObject.gameObject;
                    nearbyObject.GetComponent<BuffHandler>().AddBuff(buffInfo);
                }
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        // 销毁炸弹对象
        Destroy(gameObject);
    }
}