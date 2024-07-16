using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualKey : MonoBehaviour
{
    public bool[] getKey = new bool[5];
    public bool[] getKeyDown = new bool[5];
    public bool[] getKeyUp = new bool[5];

    public IEnumerator pressKey(int idx){
        getKey[idx] = true;
        getKeyDown[idx] = true;
        yield return null;
        getKeyDown[idx] = false;
    }
    public IEnumerator releaseKey(int idx){
        getKey[idx] = false;
        getKeyUp[idx] = true;
        yield return null;
        getKeyUp[idx] = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
