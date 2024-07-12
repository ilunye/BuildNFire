using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bomb : MonoBehaviour
{
    private float delay = 5f; // 炸弹的延迟时间
    private float explosionForce = 700f; // 爆炸的力量
    private float explosionRadius = 0.1f; // 爆炸的半径
    public GameObject explosionEffect; // 爆炸效果的预制体

    public float countdown; //爆炸倒计时
    public bool hasExploded = false;

    public BuffData buffData; //add buff

    public bool claimed = false; //是否被捡起来

    private BuffHandler buffHandler;

    // private GameObject bombvoiceObj;
    // private BombVoice bombVoice;

    void Start()
    {
        countdown = delay;
        // bombvoiceObj = GameObject.Find("bomb_voice");
        // bombVoice = bombvoiceObj.GetComponent<BombVoice>();

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
        if (other.GetComponent<Character>().playerState == Character.PlayerState.Idle)
            other.GetComponent<Character>().playerState = Character.PlayerState.ReadyToClaim;
        if (other.GetComponent<Character>().playerState == Character.PlayerState.Claim)
        {
            claimed = true;
            other.GetComponent<Character>().Material = Character.MaterialType.Bomb;
            Destroy(gameObject);
        }

    }

    public void Explode()
    {
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
}