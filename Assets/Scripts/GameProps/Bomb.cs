using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bomb : MonoBehaviour
{
    private float delay = 1f; // 炸弹的延迟时间
    private float explosionForce = 700f; // 爆炸的力量
    private float explosionRadius = 0.1f; // 爆炸的半径
    public GameObject explosionEffect; // 爆炸效果的预制体

    private float countdown;
    private bool hasExploded = false;

    public BuffData buffData;

    void Start()
    {
        countdown = delay;

    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
        else if (gameObject.transform.localPosition.y <= 0 && !hasExploded) //掉出世界爆炸
        {
            Explode();
            hasExploded = true;
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Character" && !hasExploded)
        {
            Explode();
            Debug.Log("打到敌人爆炸");
            hasExploded = true;
            Destroy(collision.gameObject);
        }

    }

    void Explode()
    {
        Debug.Log("显示爆炸效果");
        // 显示爆炸效果
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        explosionEffect = Instantiate(Resources.Load("Prefabs/Particle System") as GameObject);

        //Debug.Log(gameObject.name);
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

        Debug.Log("销毁炸弹");
        // 销毁炸弹对象
        Destroy(gameObject);
    }
}