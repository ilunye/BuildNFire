using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    public GameObject bombPrefab;  // 炸弹的预制体
    public float throwForce = 10f; // 投掷的力量

    void Start()
    {
        bombPrefab = Instantiate(Resources.Load("Prefabs/Particle System") as GameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // 检测玩家按下投掷按钮（默认是鼠标左键或Ctrl）
        {
            Throw();
        }
    }

    void Throw()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation); // 创建炸弹实例
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange); // 给炸弹一个向前的力
    }
}