using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public BuffData buffData;
    public bool delayDestroy = true;
    void Start()
    {
        if(delayDestroy){
            StartCoroutine(DestroyAfterDelay(10f));
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
