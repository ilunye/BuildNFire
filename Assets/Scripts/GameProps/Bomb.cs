using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bomb : MonoBehaviour
{
    public float delay = 2f; // 炸弹的延迟时间
    public float explosionForce = 700f; // 爆炸的力量
    public float explosionRadius = 5f; // 爆炸的半径
    public GameObject explosionEffect; // 爆炸效果的预制体

    private float countdown;
    private bool hasExploded = false;

    void Start()
    {
        countdown = delay;
        explosionEffect = Instantiate(Resources.Load("Prefabs/Bomb Red") as GameObject);
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        // 显示爆炸效果
        Instantiate(explosionEffect, transform.position, transform.rotation);

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