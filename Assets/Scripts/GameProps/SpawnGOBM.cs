using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnGOBM_", menuName = "BuffSystem/Spawn", order = 1)]
public class SpawnGOBM : BaseBuffModule //生成炸弹
{
    public GameObject prefab;

    public Vector3 localPosition;
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        var gameObject = Instantiate(prefab, buffInfo.target.transform);
        gameObject.transform.localPosition = localPosition;
    }
}
