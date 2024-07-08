using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficControl : MonoBehaviour
{
    public GameObject car1; // 这里引用你的预制体（Prefab）
    //public GameObject car2;
    //public GameObject car3;
    // 初始位置（x, y, z）
    public Vector3 spawnPosition = new Vector3(20.52f, -3.91f, 7.28f);

    void Start()
    {
        InvokeRepeating("SpawnCar", 0f, 5f); // 每隔5秒调用一次SpawnCar方法
    }

    // Update is called once per frame
    void Update()
    {
        // 这里可以添加额外的逻辑，如果需要的话
    }

    void SpawnCar()
    {
        // 从预制体生成一个新的游戏对象
        GameObject newCar = Instantiate(car1, spawnPosition, Quaternion.identity);

    }
}